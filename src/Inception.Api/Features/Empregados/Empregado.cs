namespace Inception.Api.Features.Empregados
{
    public record Empregado
    {
        public string Nome { get; init; }
        public DateTime Nascimento { get; init; }
        public int Inscricao { get; init; }
        public decimal Salario { get; init; }
        public Funcao MyProperty { get; init; }
        public Endereco Endereco { get; init; }
    }
    public enum Funcao 
    { 
        Operario = 1,
        Supervisor = 2,
        Gerente = 3
    }
    public record Endereco 
    {
        public string Rua { get; set; }
        public int Numero { get; set; }
    }
}
