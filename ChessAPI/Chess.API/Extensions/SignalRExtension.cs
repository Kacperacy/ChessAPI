using Chess.API.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Chess.API.Extensions;

public static class SignalRExtension
{
    public static IEndpointRouteBuilder UseSignalR(this IEndpointRouteBuilder app)
    {
        app.MapHub<ChessHub>("chess-hub");

        return app;
    }
}