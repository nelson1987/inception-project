using Inception.Core.Entities;
using Inception.Core.UseCases;
using Inception.Infrastructure.Persistence.Repositories;

namespace Inception.Application.Services;
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
        var newUser = new User { Username = username, Password = email, Role = string.Empty };

        _userRepository.Get(newUser.Username, newUser.Password);
        Console.WriteLine($"User {username} with the email {email} was successfully registered.");

    }
}
public class ProductManagementService { }