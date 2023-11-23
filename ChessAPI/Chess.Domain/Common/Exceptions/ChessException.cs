namespace Chess.Domain.Common.Exceptions;

public abstract class ChessException : Exception
{
    protected ChessException(string message) : base(message)
    {
    }
}