namespace Inception.Database;

public class Empregado
{
    public int Id { get; internal set; }
    public string Nome { get; internal set; }
    public string Imagem { get; internal set; }
    public decimal Salario { get; internal set; }
    public int Inscricao { get; internal set; }
    public DateTime Nascimento { get; internal set; }
    public Funcao Funcao { get; internal set; }
    public Endereco Endereco { get; internal set; }
}