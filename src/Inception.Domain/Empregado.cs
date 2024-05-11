namespace Inception.Domain;

public class Empregado
{
    public Empregado()
    {
        Endereco = new Endereco();
    }

    public int Id { get; set; }
    public string Nome { get; set; }
    public string Imagem { get; set; }
    public decimal Salario { get; set; }
    public int Inscricao { get; set; }
    public DateTime Nascimento { get; set; }
    public Funcao Funcao { get; set; }
    public int EnderecoId { get; set; }
    public Endereco Endereco { get; set; }
}