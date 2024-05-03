using Inception.Api.Features.ContasBancarias;
using Inception.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inception.Api.Features.Usuarios;
public record CriacaoUsuarioCommand
{
    public string Description { get; set; }
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
        Customer customer = new Customer()
        {
            Id = 1,
            Address = command.Description,
            City = "City",
            Email = "Email",
            FirstName = "FirstName",
            LastName = "LastName",
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
        //var server = "localhost";
        //var port = "1433"; // Default SQL Server port
        //var user = "SA"; // Warning do not use the SA account
        //var password = "numsey#2021";
        //var database = "LivrosDb";
        //var connectionString = $"Server={server}, {port};Initial Catalog={database};User ID={user};Password={password}";
        //services.AddDbContext<ApplicationDbContext>(options =>
        //    options.UseSqlServer(connectionString));
        var connectionString = "User ID=postgres;Password=postgres;Server=127.0.0.1;Port=5433;Database=basegeografica;Integrated Security=true;Pooling=true;";
        services.AddEntityFrameworkNpgsql()
            .AddDbContext<CustomersDbContext>(options =>
                options.UseNpgsql(connectionString));

        services.AddTransient<CustomersDbSeeder>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        return services;
    }
}

public class Usuario
{
    public string Description { get; set; }
}

public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    //public State State { get; set; }
    //public int Zip { get; set; }
    //public string Gender { get; set; }
    //public int OrderCount { get; set; }
    //public List<Order> Orders { get; set; }
}

//public class Order
//{
//    public int Id { get; set; }
//    public string Product { get; set; }
//    public int Quantity { get; set; }
//    public decimal Price { get; set; }
//}
//public class State
//{
//    public int Id { get; set; }
//    public string Abbreviation { get; set; }
//    public string Name { get; set; }
//}
public class CustomersDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    //public DbSet<Order> Orders { get; set; }
    //public DbSet<State> States { get; set; }

    public CustomersDbContext(DbContextOptions<CustomersDbContext> options) : base(options)
    {
    }
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    if (!optionsBuilder.IsConfigured)
    //    {
    //        optionsBuilder.UseNpgsql
    //          ("User ID=postgres;Password=postgres;Server=127.0.0.1;Port=5433;Database=Customers;Integrated Security=true;Pooling=true;");
    //    }
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id);
            //entity.Property(e => e.FirstName)
            //    .IsRequired()
            //    .HasMaxLength(100);
            //entity.Property(e => e.Address)
            //    .HasMaxLength(250);
            //entity.Property(e => e.Nascimento)
            //    .HasColumnType("date");
            //entity.Property(e => e.Salario)
            //    .HasColumnType("decimal(18, 2)");
            //entity.HasOne(e => e.State);
            //entity.HasMany(e => e.Orders);
        });
        //modelBuilder.Entity<Order>(entity =>
        //{
        //    entity.HasKey(e => e.Id);
        //});
        //modelBuilder.Entity<State>(entity =>
        //{
        //    entity.HasKey(e => e.Id);
        //});
        //OnModelCreatingPartial(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }
}


public class CustomersDbSeeder
{
    readonly ILogger _logger;

    public CustomersDbSeeder(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger("CustomersDbSeederLogger");
    }

    public async Task SeedAsync(IServiceProvider serviceProvider)
    {
        //Based on EF team's example at https://github.com/aspnet/MusicStore/blob/dev/samples/MusicStore/Models/SampleData.cs
        using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var customersDb = serviceScope.ServiceProvider.GetService<CustomersDbContext>();
            if (await customersDb.Database.EnsureCreatedAsync())
            {
                if (!await customersDb.Customers.AnyAsync())
                {
                    //await InsertCustomersSampleData(customersDb);
                }
            }
        }
    }

    //public async Task InsertCustomersSampleData(CustomersDbContext db)
    //{
    //    var states = GetStates();
    //    db.States.AddRange(states);
    //    try
    //    {
    //        await db.SaveChangesAsync();
    //    }
    //    catch (Exception exp)
    //    {
    //        _logger.LogError($"Error in {nameof(CustomersDbSeeder)}: " + exp.Message);
    //        throw;
    //    }

    //    var customers = GetCustomers(states);
    //    db.Customers.AddRange(customers);

    //    try
    //    {
    //        await db.SaveChangesAsync();
    //    }
    //    catch (Exception exp)
    //    {
    //        _logger.LogError($"Error in {nameof(CustomersDbSeeder)}: " + exp.Message);
    //        throw;
    //    }

    //}

    //private List<Customer> GetCustomers(List<State> states)
    //{
    //    //Customers
    //    var customerNames = new string[]
    //    {
    //            "Marcus,HighTower,Male,acmecorp.com",
    //            "Jesse,Smith,Female,gmail.com",
    //            "Albert,Einstein,Male,outlook.com",
    //            "Dan,Wahlin,Male,yahoo.com",
    //            "Ward,Bell,Male,gmail.com",
    //            "Brad,Green,Male,gmail.com",
    //            "Igor,Minar,Male,gmail.com",
    //            "Miško,Hevery,Male,gmail.com",
    //            "Michelle,Avery,Female,acmecorp.com",
    //            "Heedy,Wahlin,Female,hotmail.com",
    //            "Thomas,Martin,Male,outlook.com",
    //            "Jean,Martin,Female,outlook.com",
    //            "Robin,Cleark,Female,acmecorp.com",
    //            "Juan,Paulo,Male,yahoo.com",
    //            "Gene,Thomas,Male,gmail.com",
    //            "Pinal,Dave,Male,gmail.com",
    //            "Fred,Roberts,Male,outlook.com",
    //            "Tina,Roberts,Female,outlook.com",
    //            "Cindy,Jamison,Female,gmail.com",
    //            "Robyn,Flores,Female,yahoo.com",
    //            "Jeff,Wahlin,Male,gmail.com",
    //            "Danny,Wahlin,Male,gmail.com",
    //            "Elaine,Jones,Female,yahoo.com",
    //            "John,Papa,Male,gmail.com"
    //    };
    //    var addresses = new string[]
    //    {
    //            "1234 Anywhere St.",
    //            "435 Main St.",
    //            "1 Atomic St.",
    //            "85 Cedar Dr.",
    //            "12 Ocean View St.",
    //            "1600 Amphitheatre Parkway",
    //            "1604 Amphitheatre Parkway",
    //            "1607 Amphitheatre Parkway",
    //            "346 Cedar Ave.",
    //            "4576 Main St.",
    //            "964 Point St.",
    //            "98756 Center St.",
    //            "35632 Richmond Circle Apt B",
    //            "2352 Angular Way",
    //            "23566 Directive Pl.",
    //            "235235 Yaz Blvd.",
    //            "7656 Crescent St.",
    //            "76543 Moon Ave.",
    //            "84533 Hardrock St.",
    //            "5687534 Jefferson Way",
    //            "346346 Blue Pl.",
    //            "23423 Adams St.",
    //            "633 Main St.",
    //            "899 Mickey Way"
    //    };

    //    var citiesStates = new string[]
    //    {
    //            "Phoenix,AZ,Arizona",
    //            "Encinitas,CA,California",
    //            "Seattle,WA,Washington",
    //            "Chandler,AZ,Arizona",
    //            "Dallas,TX,Texas",
    //            "Orlando,FL,Florida",
    //            "Carey,NC,North Carolina",
    //            "Anaheim,CA,California",
    //            "Dallas,TX,Texas",
    //            "New York,NY,New York",
    //            "White Plains,NY,New York",
    //            "Las Vegas,NV,Nevada",
    //            "Los Angeles,CA,California",
    //            "Portland,OR,Oregon",
    //            "Seattle,WA,Washington",
    //            "Houston,TX,Texas",
    //            "Chicago,IL,Illinois",
    //            "Atlanta,GA,Georgia",
    //            "Chandler,AZ,Arizona",
    //            "Buffalo,NY,New York",
    //            "Albuquerque,AZ,Arizona",
    //            "Boise,ID,Idaho",
    //            "Salt Lake City,UT,Utah",
    //            "Orlando,FL,Florida"
    //    };

    //    var citiesIds = new int[] { 5, 9, 44, 5, 36, 17, 16, 9, 36, 14, 14, 6, 9, 24, 44, 36, 25, 19, 5, 14, 5, 23, 38, 17 };
    //    var zip = 85229;

    //    var orders = new List<Order>
    //        {
    //            new Order { Product = "Basket", Price = 29.99M, Quantity = 1 },
    //            new Order { Product = "Yarn", Price = 9.99M, Quantity = 1 },
    //            new Order { Product = "Needes", Price = 5.99M, Quantity = 1 },
    //            new Order { Product = "Speakers", Price = 499.99M, Quantity = 1 },
    //            new Order { Product = "iPod", Price = 399.99M, Quantity = 1 },
    //            new Order { Product = "Table", Price = 329.99M, Quantity = 1 },
    //            new Order { Product = "Chair", Price = 129.99M, Quantity = 4 },
    //            new Order { Product = "Lamp", Price = 89.99M, Quantity = 5 },
    //            new Order { Product = "Call of Duty", Price = 59.99M, Quantity = 1 },
    //            new Order { Product = "Controller", Price = 49.99M, Quantity = 1 },
    //            new Order { Product = "Gears of War", Price = 49.99M, Quantity = 1 },
    //            new Order { Product = "Lego City", Price = 49.99M, Quantity = 1 },
    //            new Order { Product = "Baseball", Price = 9.99M, Quantity = 5 },
    //            new Order { Product = "Bat", Price = 19.99M, Quantity = 1 }
    //        };

    //    int firstOrder, lastOrder, tempOrder = 0;
    //    var ordersLength = orders.Count;
    //    var customers = new List<Customer>();
    //    var random = new Random();

    //    for (var i = 0; i < customerNames.Length; i++)
    //    {
    //        var nameGenderHost = customerNames[i].Split(',');
    //        var cityState = citiesStates[i].Split(',');
    //        var state = states.Where(s => s.Abbreviation == cityState[1]).SingleOrDefault();

    //        var customer = new Customer
    //        {
    //            FirstName = nameGenderHost[0],
    //            LastName = nameGenderHost[1],
    //            Email = nameGenderHost[0] + '.' + nameGenderHost[1] + '@' + nameGenderHost[3],
    //            Address = addresses[i],
    //            City = cityState[0],
    //            State = state,
    //            Zip = zip + i,
    //            Gender = nameGenderHost[2],
    //            OrderCount = 0
    //        };

    //        firstOrder = (int)Math.Floor(random.NextDouble() * orders.Count);
    //        lastOrder = (int)Math.Floor(random.NextDouble() * orders.Count);

    //        if (firstOrder > lastOrder)
    //        {
    //            tempOrder = firstOrder;
    //            firstOrder = lastOrder;
    //            lastOrder = tempOrder;
    //        }

    //        customer.Orders = new List<Order>();

    //        for (var j = firstOrder; j <= lastOrder && j < ordersLength; j++)
    //        {
    //            var order = new Order
    //            {
    //                Product = orders[j].Product,
    //                Price = orders[j].Price,
    //                Quantity = orders[j].Quantity
    //            };
    //            customer.Orders.Add(order);
    //        }
    //        customer.OrderCount = customer.Orders.Count;
    //        customers.Add(customer);
    //    }

    //    return customers;
    //}

    //private List<State> GetStates()
    //{
    //    var states = new List<State>
    //        {
    //            new State { Name = "Alabama", Abbreviation = "AL" },
    //            new State { Name = "Montana", Abbreviation = "MT" },
    //            new State { Name = "Alaska", Abbreviation = "AK" },
    //            new State { Name = "Nebraska", Abbreviation = "NE" },
    //            new State { Name = "Arizona", Abbreviation = "AZ" },
    //            new State { Name = "Nevada", Abbreviation = "NV" },
    //            new State { Name = "Arkansas", Abbreviation = "AR" },
    //            new State { Name = "New Hampshire", Abbreviation = "NH" },
    //            new State { Name = "California", Abbreviation = "CA" },
    //            new State { Name = "New Jersey", Abbreviation = "NJ" },
    //            new State { Name = "Colorado", Abbreviation = "CO" },
    //            new State { Name = "New Mexico", Abbreviation = "NM" },
    //            new State { Name = "Connecticut", Abbreviation = "CT" },
    //            new State { Name = "New York", Abbreviation = "NY" },
    //            new State { Name = "Delaware", Abbreviation = "DE" },
    //            new State { Name = "North Carolina", Abbreviation = "NC" },
    //            new State { Name = "Florida", Abbreviation = "FL" },
    //            new State { Name = "North Dakota", Abbreviation = "ND" },
    //            new State { Name = "Georgia", Abbreviation = "GA" },
    //            new State { Name = "Ohio", Abbreviation = "OH" },
    //            new State { Name = "Hawaii", Abbreviation = "HI" },
    //            new State { Name = "Oklahoma", Abbreviation = "OK" },
    //            new State { Name = "Idaho", Abbreviation = "ID" },
    //            new State { Name = "Oregon", Abbreviation = "OR" },
    //            new State { Name = "Illinois", Abbreviation = "IL" },
    //            new State { Name = "Pennsylvania", Abbreviation = "PA" },
    //            new State { Name = "Indiana", Abbreviation = "IN" },
    //            new State { Name = "Rhode Island", Abbreviation = "RI" },
    //            new State { Name = "Iowa", Abbreviation = "IA" },
    //            new State { Name = "South Carolina", Abbreviation = "SC" },
    //            new State { Name = "Kansas", Abbreviation = "KS" },
    //            new State { Name = "South Dakota", Abbreviation = "SD" },
    //            new State { Name = "Kentucky", Abbreviation = "KY" },
    //            new State { Name = "Tennessee", Abbreviation = "TN" },
    //            new State { Name = "Louisiana", Abbreviation = "LA" },
    //            new State { Name = "Texas", Abbreviation = "TX" },
    //            new State { Name = "Maine", Abbreviation = "ME" },
    //            new State { Name = "Utah", Abbreviation = "UT" },
    //            new State { Name = "Maryland", Abbreviation = "MD" },
    //            new State { Name = "Vermont", Abbreviation = "VT" },
    //            new State { Name = "Massachusetts", Abbreviation = "MA" },
    //            new State { Name = "Virginia", Abbreviation = "VA" },
    //            new State { Name = "Michigan", Abbreviation = "MI" },
    //            new State { Name = "Washington", Abbreviation = "WA" },
    //            new State { Name = "Minnesota", Abbreviation = "MN" },
    //            new State { Name = "West Virginia", Abbreviation = "WV" },
    //            new State { Name = "Mississippi", Abbreviation = "MS" },
    //            new State { Name = "Wisconsin", Abbreviation = "WI" },
    //            new State { Name = "Missouri", Abbreviation = "MO" },
    //            new State { Name = "Wyoming", Abbreviation = "WY" }
    //        };

    //    return states;
    //}
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

        await service.Handle(request, cancellationToken);

        //return Unauthorized();
        return Created();
    }
}