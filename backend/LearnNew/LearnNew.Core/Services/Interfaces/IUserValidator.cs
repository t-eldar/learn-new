namespace LearnNew.Core.Services.Interfaces;
public interface IUserValidator
{
    bool ValidatePassword(string password);
    bool ValidateEmail(string email);
    bool ValidateName(string name);
    bool ValidateSurname(string surname);
}
