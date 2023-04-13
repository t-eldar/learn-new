using LearnNew.Models.Authentication;

namespace LearnNew.Core.Repositories.Interfaces;
public interface IUserRepository
{
    Task<User> CreateAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByEmailAsync(string email);
    Task<IEnumerable<User>?> GetAllAsync();
}
