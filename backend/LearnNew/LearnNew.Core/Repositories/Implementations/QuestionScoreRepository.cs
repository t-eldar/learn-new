using LearnNew.Core.DatabaseContext;
using LearnNew.Core.Repositories.Interfaces;
using LearnNew.Models.Core;

using Microsoft.EntityFrameworkCore;

namespace LearnNew.Core.Repositories.Implementations;
public class QuestionScoreRepository : IQuestionScoreRepository
{
    private readonly ApplicationContext _applicationContext;

    public QuestionScoreRepository(ApplicationContext applicationContext) => _applicationContext = applicationContext;
    public async Task<IEnumerable<QuestionScore>?> GetAllAsync() => await _applicationContext
        .QuestionScores
        .Include(s => s.Question!)
        .Include(s => s.TestScore!)
        .ToArrayAsync();
    public async Task<QuestionScore?> GetByIdAsync(int id) => await _applicationContext
        .QuestionScores
        .Include(s => s.Question!)
        .Include(s => s.TestScore!)
        .FirstOrDefaultAsync(s => s.Id == id);
    public async Task<QuestionScore?> GetByUserAndQuestionIdAsync(int userId, int questionId) => await _applicationContext
        .QuestionScores
        .Include(s => s.Question!)
        .Include(s => s.TestScore!)
        .FirstOrDefaultAsync(s => s.UserId == userId && s.QuestionId == questionId);
    public async Task<IEnumerable<QuestionScore>?> GetByTestScoreIdAsync(int testScoreId) => await _applicationContext
        .QuestionScores
        .Include(s => s.Question!)
        .Include(s => s.TestScore!)
        .Where(s => s.TestScoreId == testScoreId)
        .ToArrayAsync();
    public async Task<QuestionScore> CreateAsync(QuestionScore questionScore)
    {
        ArgumentNullException.ThrowIfNull(questionScore);

        var existed = GetByUserAndQuestionIdAsync(questionScore.UserId, questionScore.QuestionId);
        if (existed is not null)
        {
            throw new Exception("QuestionScore already exist");
        }

        await _applicationContext.QuestionScores.AddAsync(questionScore);
        await _applicationContext.SaveChangesAsync();

        return questionScore;
    }
    public async Task DeleteAsync(int id)
    {
        var existed = await GetByIdAsync(id);
        if (existed is null)
        {
            return;
        }

        _applicationContext.QuestionScores.Remove(existed);
        await _applicationContext.SaveChangesAsync();
    }
}
