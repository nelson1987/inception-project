namespace Inception.Database;

//public class Usuario
//{
//    public int Id { get; internal set; }
//    public string Nome { get; internal set; }
//    public string Login { get; internal set; }
//    public string Senha { get; internal set; }
//    public bool Ativo { get; internal set; }
//    public string Email { get; internal set; }
//}
public record Endereco
{
    public int Id { get; internal set; }
    public string Rua { get; internal set; }
    public int Numero { get; internal set; }
}