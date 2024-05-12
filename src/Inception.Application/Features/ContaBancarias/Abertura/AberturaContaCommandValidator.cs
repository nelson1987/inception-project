using FluentValidation;

namespace Inception.Application.Features.ContaBancarias.Abertura;

public class AberturaContaCommandValidator : AbstractValidator<AberturaContaCommand>
{
    public AberturaContaCommandValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Name).Length(0, 10);
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Age).InclusiveBetween(18, 60);
    }
};