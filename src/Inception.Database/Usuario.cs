namespace Inception.Database;

public class Usuario
{
    public int Id { get; internal set; }
    public string Nome { get; internal set; }
    public string Login { get; internal set; }
    public string Senha { get; internal set; }
    public bool Ativo { get; internal set; }
    public string Email { get; internal set; }
}
