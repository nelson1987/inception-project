using InceptionClean.Application.Abstractions;
using InceptionClean.Domain.Entities;
using InceptionClean.Infrastructure.DbContexts;

namespace InceptionClean.Infrastructure.Repositories;
public class PersonRepository : IPersonRepository
{
    private readonly PersonDbContext _context;

    public PersonRepository(PersonDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Person>> GetAll()
    {
        throw new NotImplementedException();
        //return await _context.Person.ToListAsync();
    }

    public async Task<Person> AddPerson(Person toCreate)
    {
        return await Task.FromResult(new Person { Id = toCreate.Id, Name = toCreate.Name });
        //_context.Person.Add(toCreate);

        //await _context.SaveChangesAsync();

        //return toCreate;
    }
}