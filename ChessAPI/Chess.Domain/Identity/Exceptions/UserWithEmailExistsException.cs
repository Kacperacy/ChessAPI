using Chess.Domain.Common.Exceptions;

namespace Chess.Domain.Identity.Exceptions;

public class UserWithEmailExistsException : ChessException
{
    public UserWithEmailExistsException() : base("User with this email already exists.")
    {
    }
}