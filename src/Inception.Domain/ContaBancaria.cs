namespace Inception.Domain;

public class ContaBancaria
{
    public int Id { get; set; }
    public required string Numero { get; set; }
    public required string Agencia { get; set; }
    public required string CodigoCompe { get; set; }
    public required string Documento { get; set; }
    public decimal Saldo { get; set; }
    public required List<Movimentacao> Movimentacoes { get; set; }
}