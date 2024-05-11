namespace Inception.Domain;

public record Endereco
{
    public int Id { get; set; }
    public string Rua { get; set; }
    public int Numero { get; set; }
    public bool Ativo { get; set; }
}