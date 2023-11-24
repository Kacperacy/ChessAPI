using Chess.Infrastructure.Common.Hubs;

namespace Chess.API.Extensions;

public static class SignalRExtension
{
    public static IEndpointRouteBuilder UseSignalR(this IEndpointRouteBuilder app)
    {
        app.MapHub<ChessHub>("chessHub");

        return app;
    }
}