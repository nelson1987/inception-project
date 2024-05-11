namespace Inception.Api.Features.Account.Login;

public record LoginAccountCommand
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}
