using LearnNew.Models.Core;

namespace LearnNew.Core.Repositories.Interfaces;
public interface ITestScoreRepository
{
    Task<IEnumerable<TestScore>?> GetAllAsync();
    Task<TestScore?> GetByUserAndTestIdAsync(int userId, int testId);
    Task<TestScore?> GetByIdAsync(int id);
    Task<TestScore> CreateAsync(TestScore testScore);
    Task UpdateAsync(TestScore testScore);
    Task DeleteAsync(int id);
}
