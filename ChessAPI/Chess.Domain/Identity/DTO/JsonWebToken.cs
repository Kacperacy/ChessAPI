namespace Chess.Domain.Identity.DTO;

public sealed class JsonWebToken
{
    public string AccessToken { get; set; }
    public long Expires { get; set; }
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public ICollection<string>? Roles { get; init; }
    public IDictionary<string, string>? Claims { get; init; }
}