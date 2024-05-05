using Inception.Api.Features.ContasBancarias;
using Inception.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inception.Api.Features.Usuarios;
public record CriacaoUsuarioCommand
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Address { get; set; }
    public required string City { get; set; }
}

public interface IUsuarioService
{
    Task Handle(CriacaoUsuarioCommand command, CancellationToken cancellationToken = default);
}

public class UsuarioService : IUsuarioService
{
    private readonly CustomersDbContext _context;
    private readonly ILogger _logger;
    public UsuarioService(CustomersDbContext context, ILoggerFactory loggerFactory)
    {
        _context = context;
        _logger = loggerFactory.CreateLogger("CustomersRepository");
    }

    public async Task Handle(CriacaoUsuarioCommand command, CancellationToken cancellationToken = default)
    {
        var listagem = await GetCustomersAsync();
        Customer customer = new Customer()
        {
            Id = command.Id,
            Address = command.FirstName,
            City = command.City,
            Email = command.Email,
            FirstName = command.FirstName,
            LastName = command.LastName
        };
        await InsertCustomerAsync(customer);
        //throw new NotImplementedException();
        //return Task.CompletedTask;
    }
    public async Task<List<Customer>> GetCustomersAsync()
    {
        return await _context.Customers.OrderBy(c => c.LastName).ToListAsync();
    }

    public async Task<Customer> GetCustomerAsync(int id)
    {
        return await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
    }

    //public async Task<List<State>> GetStatesAsync()
    //{
    //    return await _context.States.OrderBy(s => s.Abbreviation).ToListAsync();
    //}

    public async Task<Customer> InsertCustomerAsync(Customer customer)
    {
        _context.Add(customer);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (System.Exception exp)
        {
            _logger.LogError($"Error in {nameof(InsertCustomerAsync)}: " + exp.Message);
        }

        return customer;
    }

    public async Task<bool> UpdateCustomerAsync(Customer customer)
    {
        //Will update all properties of the Customer
        _context.Customers.Attach(customer);
        _context.Entry(customer).State = EntityState.Modified;
        try
        {
            return (await _context.SaveChangesAsync() > 0 ? true : false);
        }
        catch (Exception exp)
        {
            _logger.LogError($"Error in {nameof(UpdateCustomerAsync)}: " + exp.Message);
        }
        return false;
    }

    public async Task<bool> DeleteCustomerAsync(int id)
    {
        //Extra hop to the database but keeps it nice and simple for this demo
        var customer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
        _context.Remove(customer);
        try
        {
            return (await _context.SaveChangesAsync() > 0 ? true : false);
        }
        catch (System.Exception exp)
        {
            _logger.LogError($"Error in {nameof(DeleteCustomerAsync)}: " + exp.Message);
        }
        return false;
    }
}

public static class Dependencies
{
    public static IServiceCollection AddUsuarioInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEntityFrameworkNpgsql()
            .AddDbContext<CustomersDbContext>(options =>
                options.UseNpgsql(configuration["Data:DbContext:CustomersConnectionString"]));

        services.AddTransient<CustomersDbSeeder>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        return services;
    }
}

public class Usuario
{
    public required string Description { get; set; }
}

[Route("api/[controller]")]
public class UsuariosController : DefaulController
{
    public UsuariosController(ILogger<UsuariosController> logger)
        : base(logger)
    {
    }

    //POST
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CriacaoUsuarioCommand request,
    [FromServices] IUsuarioService service,
    //[FromServices] IValidator<AberturaContaCommand> validator,
    CancellationToken cancellationToken = default)
    {
        //var validationResult = await validator.ValidateAsync(request, cancellationToken);
        //if (!validationResult.IsValid)
        //    return UnprocessableEntity(validationResult.ToModelState());

        //if (!ModelState.IsValid)
        //    return BadRequest(ModelState);
        request = request with { Address = "Address", City = "City", Email = "Email", FirstName = "FirstName", LastName = "LastName" };
        await service.Handle(request, cancellationToken);

        //return Unauthorized();
        return Created();
    }
}