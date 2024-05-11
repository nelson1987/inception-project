namespace Inception.Application.Features.LoginAccount;

public record LoginAccountCommand
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}
