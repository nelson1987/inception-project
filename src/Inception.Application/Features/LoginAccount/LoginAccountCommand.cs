namespace Inception.Application.Features.LoginAccount;

public record LoginAccountCommand
{
    public string Username { get; set; }
    public string Password { get; set; }
}
