using Swashbuckle.AspNetCore.Annotations;

namespace Inception.Database;

public class Empregado
{
    [SwaggerSchema(Description = "Identificador Unico", Nullable = false)]
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Imagem { get; set; }
    public decimal Salario { get; set; }
    public int Inscricao { get; set; }
    public DateTime Nascimento { get; set; }
    public Funcao Funcao { get; set; }
    public Endereco Endereco { get; set; }
}