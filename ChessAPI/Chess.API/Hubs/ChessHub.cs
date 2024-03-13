using Microsoft.AspNetCore.SignalR;

namespace Chess.API.Hubs;

public sealed class ChessHub : Hub
{
    private enum Color
    {
        White = 1,
        Black = 2
    }
    
    private readonly Dictionary<Color, string> ColorToString = new()
    {
        {Color.White, "w"},
        {Color.Black, "b"}
    };
    
    private class ConnectedUser
    {
        public string ConnectionId { get; set; } = "";
        public Guid? GameId { get; set; }
        public Color? Color { get; set; }
    }

    private static List<ConnectedUser> _connectedUsers { get; set; } = new();
    public override async Task OnConnectedAsync()
    {
        try
        {
            _connectedUsers.Add(new ConnectedUser()
            { 
                ConnectionId  = Context.ConnectionId,
                GameId = null
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        try
        {
            var user = _connectedUsers.FirstOrDefault(p => p.ConnectionId == Context.ConnectionId);
            if (user is null)
                return;

            if (user.GameId is not null)
            {
                var opponent = _connectedUsers.FirstOrDefault(p => p.GameId == user.GameId && p.ConnectionId != Context.ConnectionId);
                if (opponent is not null)
                {
                    opponent.GameId = null;
                    await Clients.Client(opponent.ConnectionId).SendAsync("OpponentDisconnected");
                    await Groups.RemoveFromGroupAsync(user.GameId.ToString(), opponent.ConnectionId);
                }
            }
            
            _connectedUsers.Remove(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task FindOpponent()
    {
        try
        {
            var currentUser = _connectedUsers.FirstOrDefault(p => p.ConnectionId == Context.ConnectionId);

            if (currentUser is null)
                return;

            if (currentUser.GameId is not null)
            {
                await Clients.Client(currentUser.ConnectionId).SendAsync("GameFound", currentUser.GameId);
                return;
            }

            var userWithoutGame = _connectedUsers
                .FirstOrDefault(p => p.GameId is null && p.ConnectionId != Context.ConnectionId);

            if (userWithoutGame is null)
                return;

            var gameId = Guid.NewGuid();

            userWithoutGame.GameId = gameId;
            currentUser.GameId = gameId;

            await Groups.AddToGroupAsync(userWithoutGame.ConnectionId, gameId.ToString());
            await Groups.AddToGroupAsync(currentUser.ConnectionId, gameId.ToString());

            var random = new Random();
            var randomColor = random.Next(1, 2);
            
            userWithoutGame.Color = (Color) randomColor;
            currentUser.Color = (Color) (randomColor == 1 ? 2 : 1);
            
            await Clients.Client(userWithoutGame.ConnectionId).SendAsync("GameFound", gameId, ColorToString[userWithoutGame.Color.Value]);
            await Clients.Client(currentUser.ConnectionId).SendAsync("GameFound", gameId, ColorToString[currentUser.Color.Value]);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task Move(string gameCode, string move)
    {
        var opponent = _connectedUsers.FirstOrDefault(p => p.GameId.ToString() == gameCode && p.ConnectionId != Context.ConnectionId);
        if(opponent is not null)
            await Clients.Client(opponent.ConnectionId).SendAsync("Move", move);
    }
}   