using Chess.Domain.Common.Exceptions;

namespace Chess.Domain.Games.Exceptions;

public sealed class UserNotFoundException : ChessException
{
    public UserNotFoundException() : base("User not found.")
    {
    }
}