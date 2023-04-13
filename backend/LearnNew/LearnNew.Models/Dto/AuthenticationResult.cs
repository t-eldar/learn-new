using System.Security.Claims;

namespace LearnNew.Models.Dto;
public class AuthenticationResult
{
    public ClaimsPrincipal? ClaimsPrincipal { get; set; }
    public string? Error { get; set; }
}
