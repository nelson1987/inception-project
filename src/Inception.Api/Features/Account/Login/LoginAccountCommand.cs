namespace Inception.Api.Features.Account.Login;

public record LoginAccountCommand
{
    public string Username { get; set; }
    public string Password { get; set; }
}
