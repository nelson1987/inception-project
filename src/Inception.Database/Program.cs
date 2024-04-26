// See https://aka.ms/new-console-template for more information
using Inception.Database;

Console.WriteLine("Hello, World!");
AppDbContext context = new AppDbContext();
await context.Produtos.AddAsync(new Produto());
await context.Usuarios.AddAsync(new Usuario());
await context.SaveChangesAsync();