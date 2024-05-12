namespace Inception.Database;

public record Endereco
{
    public int Id { get; internal set; }
    public string? Rua { get; internal set; }
    public int Numero { get; internal set; }
    public bool Ativo { get; internal set; }
}