using LearnNew.Core.DatabaseContext;
using LearnNew.Core.Repositories.Interfaces;
using LearnNew.Models.Core;

using Microsoft.EntityFrameworkCore;

namespace LearnNew.Core.Repositories.Implementations;
public class TestScoreRepository : ITestScoreRepository
{
    private readonly ApplicationContext _applicationContext;

    public TestScoreRepository(ApplicationContext applicationContext) => _applicationContext = applicationContext;

    public async Task<IEnumerable<TestScore>?> GetAllAsync() => await _applicationContext
        .TestScores.ToArrayAsync();
    public async Task<TestScore?> GetByIdAsync(int id) => await _applicationContext
        .TestScores.FirstOrDefaultAsync(s => s.Id == id);
    public async Task<TestScore?> GetByUserAndTestIdAsync(int userId, int testId) => await _applicationContext
        .TestScores.FirstOrDefaultAsync(s => s.UserId == userId && s.TestId == testId);
    public async Task<TestScore> CreateAsync(TestScore testScore)
    {
        ArgumentNullException.ThrowIfNull(testScore);

        var existed = GetByUserAndTestIdAsync(testScore.UserId, testScore.TestId);

        if (existed is not null)
        {
            throw new Exception("TestScore already exist");
        }

        await _applicationContext.TestScores.AddAsync(testScore);
        await _applicationContext.SaveChangesAsync();

        return testScore;
    }
    public async Task UpdateAsync(TestScore testScore)
    {
        ArgumentNullException.ThrowIfNull(testScore);

        var existed = await _applicationContext.TestScores
            .FirstOrDefaultAsync(s => s.Id == testScore.Id) 
            ?? throw new Exception("Test score is not exist");

        existed.TestingDate = testScore.TestingDate;
        existed.UserId = testScore.UserId;
        existed.TestId = testScore.TestId;
        existed.Score = testScore.Score;

        await _applicationContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var testScore = await _applicationContext.TestScores.FirstOrDefaultAsync(t => t.Id == id);
        if (testScore is null)
        {
            return;
        }

        _applicationContext.TestScores.Remove(testScore);
        await _applicationContext.SaveChangesAsync();
    }
}
