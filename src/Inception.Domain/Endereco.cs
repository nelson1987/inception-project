namespace Inception.Core;

public record Endereco
{
    public int Id { get; set; }
    public required string Rua { get; set; }
    public int Numero { get; set; }
    public bool Ativo { get; set; }
}