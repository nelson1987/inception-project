using InceptionClean.Domain.Entities;

namespace InceptionClean.Application.Abstractions;
public interface IPersonRepository
{
    Task<ICollection<Person>> GetAll();
    Task<Person> AddPerson(Person toCreate);
}