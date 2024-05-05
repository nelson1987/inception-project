namespace Inception.Core;
public class Movimentacao
{
    public int Id { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataMovimentacao { get; set; }
    public required ContaBancaria Conta { get; set; }
}