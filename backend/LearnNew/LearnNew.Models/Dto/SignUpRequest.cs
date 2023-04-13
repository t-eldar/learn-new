namespace LearnNew.Models.Dto;
public record SignUpRequest(
    string Name,
    string Surname,
    string Email,
    string Password);
