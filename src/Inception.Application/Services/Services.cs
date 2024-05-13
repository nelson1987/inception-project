using Structure.Core.Entities;
using Structure.Core.UseCases;
using Structure.Infrastructure.Repositories;

namespace Structure.Application.Services;
public class UserRegistrationService : IUserRegistrationUseCase
{
    private readonly IUserRepository _userRepository;

    public UserRegistrationService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void RegisterUser(string username, string email)
    {
        // The user registration logic is implemented here
        var newUser = new User { Username = username, Email = email };

        _userRepository.AddUser(newUser);
        Console.WriteLine($"User {username} with the email {email} was successfully registered.");

    }
}
public class ProductManagementService { }