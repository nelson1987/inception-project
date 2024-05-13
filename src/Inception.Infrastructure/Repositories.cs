using Structure.Core.Entities;

namespace Structure.Infrastructure.Repositories;
public interface IUserRepository
{
    void AddUser(User user);
}
public class UserRepository : IUserRepository
{
    public void AddUser(User user)
    {
        // This is where the logic for adding a user to the database would be implemented
        Console.WriteLine($"User {user.Username} has been added to the database.");
    }
}
