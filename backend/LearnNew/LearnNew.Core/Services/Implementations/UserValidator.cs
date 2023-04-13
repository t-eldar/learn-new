using System.Text.RegularExpressions;

using LearnNew.Core.Services.Interfaces;

namespace LearnNew.Core.Services.Implementations;
public class UserValidator : IUserValidator
{
    public bool ValidateEmail(string email)
    {
        var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,4})+)$");
        var match = regex.Match(email);
        if (match.Success)
        {
            return true;
        }

        return false;
    }
    public bool ValidateName(string name)
    {
        var regex = new Regex(@"^[A-ZА-Я][a-zа-я]*$");

        var match = regex.Match(name);
        if (match.Success)
        {
            return true;
        }

        return false;
    }
    public bool ValidatePassword(string password)
    {
        // Minimum eight characters, at least one letter and one number
        var regex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$");

        var match = regex.Match(password);
        if (match.Success)
        {
            return true;
        }

        return false;
    }
    public bool ValidateSurname(string surname)
    {
        var regex = new Regex(@"^[A-ZА-Я][a-zа-я]*$");

        var match = regex.Match(surname);
        if (match.Success)
        {
            return true;
        }

        return false;
    }
}
