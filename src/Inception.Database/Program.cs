// See https://aka.ms/new-console-template for more information
using Inception.Database;

Console.WriteLine("Hello, World!");
AppDbContext context = new AppDbContext();
await context.Produtos.AddAsync(new Empregado() { Funcao = Funcao.Gerente, Id = 2, Endereco = new Endereco() { Id = 1 } });
//await context.Enderecos.AddAsync(new Endereco());
await context.SaveChangesAsync();