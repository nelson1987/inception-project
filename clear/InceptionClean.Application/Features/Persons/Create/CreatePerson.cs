using FluentValidation;
using InceptionClean.Application.Abstractions;
using InceptionClean.Application.Extensions;
using InceptionClean.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Windows.Input;

namespace InceptionClean.Application.Features.Persons.Create;

public record CreatePersonRequest
{
    public string? Name { get; set; }
    public string? Email { get; set; }
};

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
public class CreatePerson : ICommand<Person>
{
    public string? Name { get; set; }
    public string? Email { get; set; }
}
public class CreatePersonHandler : IRequestHandler<CreatePerson, Person>
{
    private readonly ILogger<CreatePersonHandler> _logger;
    private readonly IPersonRepository _personRepository;

    public CreatePersonHandler(ILogger<CreatePersonHandler> logger, IPersonRepository personRepository)
    {
        _logger = logger;
        _personRepository = personRepository;
    }

    public async Task<Person> Handle(CreatePerson request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Started command executed Json:\n{request.ToJson()}");
        var person = new Person
        {
            Name = request.Name,
            Email = request.Email
        };
        _logger.LogInformation($"Ended command executed Json:\n{person.ToJson()}");
        return await _personRepository.AddPerson(person);
    }
}
public class CreatePersonValidator : AbstractValidator<CreatePerson>
{
    public CreatePersonValidator()
    {
        RuleFor(x => x.Name).Length(0, 10);
        RuleFor(x => x.Email).EmailAddress();
    }
};