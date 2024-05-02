using FluentValidation;
using Inception.Api.Contracts;

namespace Inception.Api.Features.Empregados.Create;

public class CreateEmpregadoValidator : AbstractValidator<CreateEmpregadoRequest>
{
    public CreateEmpregadoValidator()
    {
        RuleFor(x => x.Inscricao).GreaterThan(0);
        RuleFor(x => x.Nome).NotEmpty();
    }
}