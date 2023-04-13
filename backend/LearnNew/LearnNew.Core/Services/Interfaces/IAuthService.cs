using System.Security.Claims;

using LearnNew.Models.Dto;

namespace LearnNew.Core.Services.Interfaces;
public interface IAuthService
{
    Task<AuthenticationResult> SignUpAsync(string name, string surname, string email, string password);
    Task<AuthenticationResult> SignInAsync(string email, string password);
}
