namespace Inception.Core.Entities;

public class Empregado
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required string Imagem { get; set; }
    public decimal Salario { get; set; }
    public int Inscricao { get; set; }
    public DateTime Nascimento { get; set; }
    public Funcao Funcao { get; set; }
    public int EnderecoId { get; set; }
    public required Endereco Endereco { get; set; }
}