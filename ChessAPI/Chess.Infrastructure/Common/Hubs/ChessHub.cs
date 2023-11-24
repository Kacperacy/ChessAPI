using Microsoft.AspNetCore.SignalR;

namespace Chess.Infrastructure.Common.Hubs;

public sealed class ChessHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} joined the chat");
    }

    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} : {message}");
    }
}