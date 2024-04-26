namespace Inception.Database;

public class Produto
{
    public int Id { get; internal set; }
    public string Nome { get; internal set; }
    public decimal Preco { get; internal set; }
    public int Estoque { get; internal set; }
    public string Imagem { get; internal set; }
}
