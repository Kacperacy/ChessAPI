using Chess.Domain.Common.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Chess.Domain.Identity.Exceptions;

public sealed class CreateUserException : ChessException
{
    public IDictionary<string, string[]> Errors { get; }
    
    public CreateUserException() : base("One or more errors occurred during creating user.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public CreateUserException(IEnumerable<IdentityError> errors) : this()
    {
        Errors = errors.GroupBy(e => e.Code, e => e.Description)
            .ToDictionary(a => a.Key, a => a.ToArray());
    }

}