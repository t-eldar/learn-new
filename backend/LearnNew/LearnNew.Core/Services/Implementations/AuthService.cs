using System.Security.Claims;

using LearnNew.Core.Repositories.Interfaces;
using LearnNew.Core.Services.Interfaces;
using LearnNew.Models.Authentication;
using LearnNew.Models.Dto;

using Microsoft.AspNetCore.Identity;

namespace LearnNew.Core.Services.Implementations;
public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserValidator _userValidator;
    private readonly IPasswordHasher<User> _passwordHasher;
    public AuthService(
        IUserRepository userRepository,
        IUserValidator userValidator,
        IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _userValidator = userValidator;
        _passwordHasher = passwordHasher;
    }

    public async Task<AuthenticationResult> SignUpAsync(string name, string surname, string email, string password)
    {
        if (!_userValidator.ValidateName(name) || !_userValidator.ValidateSurname(surname)
            || !_userValidator.ValidateEmail(email) || !_userValidator.ValidatePassword(password))
        {
            return new AuthenticationResult { Error = "Bad credentials" };
        }

        var user = new User
        {
            Name = name,
            Surname = surname,
            Email = email,
        };
        user.PasswordHash = _passwordHasher.HashPassword(user, password);

        var newUser = await _userRepository.CreateAsync(user);
        var principal = ConvertToClaim(newUser);

        return new AuthenticationResult { ClaimsPrincipal = principal, };
    }
    public async Task<AuthenticationResult> SignInAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        if (user is null)
        {
            return new AuthenticationResult { Error = "Bad credentials" };
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

        if (result == PasswordVerificationResult.Failed)
        {
            return new AuthenticationResult { Error = "Bad credentials" };
        }

        var princinpal = ConvertToClaim(user);

        return new AuthenticationResult { ClaimsPrincipal = princinpal };
    }

    private ClaimsPrincipal ConvertToClaim(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim ("email", user.Email),
            new Claim("name", user.Name),
            new Claim("surname", user.Surname),
            new Claim("id", user.Id.ToString()),
        };
        var identity = new ClaimsIdentity(claims, "Cookies");

        return new ClaimsPrincipal(identity);
    }
}
