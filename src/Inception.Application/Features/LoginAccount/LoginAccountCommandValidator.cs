using FluentValidation;

namespace Inception.Application.Features.LoginAccount;

public class LoginAccountCommandValidator : AbstractValidator<LoginAccountCommand>
{
    public LoginAccountCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty();
        RuleFor(x => x.Password)
            .NotEmpty();
    }
}
