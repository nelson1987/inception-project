namespace Inception.Api.Features.ContasBancarias.Abertura;

public record AberturaContaCommand
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public int Age { get; init; }
};
