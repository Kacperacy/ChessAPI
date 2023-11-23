using Chess.Domain.Common.Exceptions;

namespace Chess.Domain.Identity.Exceptions;

public class AddToRoleException : ChessException
{
    public AddToRoleException() : base("Error while adding user to role.")
    {
    }
}