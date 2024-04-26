// See https://aka.ms/new-console-template for more information
using Inception.Database;

Console.WriteLine("Hello, World!");
AppDbContext context = new AppDbContext();
await context.Produtos.AddAsync(new Empregado());
await context.Enderecos.AddAsync(new Endereco());
await context.SaveChangesAsync();