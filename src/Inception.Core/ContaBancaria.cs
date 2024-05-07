namespace Inception.Core
{
    public class ContaBancaria
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public string Agencia { get; set; }
        public string CodigoCompe { get; set; }
        public string Documento { get; set; }
        public decimal Saldo { get; set; }
        public List<Movimentacao> Movimentacoes { get; set; }
    }
}