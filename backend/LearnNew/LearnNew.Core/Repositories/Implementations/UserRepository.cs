using LearnNew.Core.DatabaseContext;
using LearnNew.Core.Repositories.Interfaces;
using LearnNew.Models.Authentication;

using Microsoft.EntityFrameworkCore;

namespace LearnNew.Core.Repositories.Implementations;
public class UserRepository : IUserRepository
{
    private readonly ApplicationContext _applicationContext;

    public UserRepository(ApplicationContext applicationContext) => _applicationContext = applicationContext;
    public async Task<IEnumerable<User>?> GetAllAsync() => await _applicationContext
        .Users
        .ToArrayAsync();
    public async Task<User?> GetByIdAsync(int id) => await _applicationContext
        .Users
        .FirstOrDefaultAsync(u => u.Id == id);
    public async Task<User?> GetByEmailAsync(string email) => await _applicationContext
        .Users
        .FirstOrDefaultAsync(u => u.Email == email);

    public async Task<User> CreateAsync(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        var existed = await GetByEmailAsync(user.Email);
        if (existed != null)
        {
            throw new Exception("User already exists");
        }

        await _applicationContext.Users.AddAsync(user);
        await _applicationContext.SaveChangesAsync();
        return user;
    }
    public async Task DeleteAsync(int id)
    {
        var existed = await GetByIdAsync(id);
        if (existed is null)
        {
            return;
        }

        _applicationContext.Users.Remove(existed);
        await _applicationContext.SaveChangesAsync();
    }
    public async Task UpdateAsync(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        var existed = await GetByIdAsync(user.Id)
            ?? throw new Exception("User not found");
        existed.Name = user.Name;
        existed.Surname = user.Surname;
        existed.Email = user.Email;

        await _applicationContext.SaveChangesAsync();
    }
}
