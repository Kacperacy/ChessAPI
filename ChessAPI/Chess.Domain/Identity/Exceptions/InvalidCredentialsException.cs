using Chess.Domain.Common.Exceptions;

namespace Chess.Domain.Identity.Exceptions;

public class InvalidCredentialsException : ChessException
{
    public InvalidCredentialsException() : base("Invalid credentials.")
    {
    }
}