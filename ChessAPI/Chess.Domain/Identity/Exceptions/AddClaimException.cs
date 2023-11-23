using Chess.Domain.Common.Exceptions;

namespace Chess.Domain.Identity.Exceptions;

public class AddClaimException : ChessException
{
    public AddClaimException() : base("Error while adding claim to user.")
    {
    }
}