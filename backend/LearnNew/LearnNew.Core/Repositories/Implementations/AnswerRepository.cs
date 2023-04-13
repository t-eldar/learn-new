using LearnNew.Core.DatabaseContext;
using LearnNew.Core.Repositories.Interfaces;
using LearnNew.Models.Core;

using Microsoft.EntityFrameworkCore;

namespace LearnNew.Core.Repositories.Implementations;
public class AnswerRepository : IAnswerRepository
{
    private readonly ApplicationContext _applicationContext;
    public AnswerRepository(ApplicationContext applicationContext) => _applicationContext = applicationContext;

    public async Task<Answer?> GetCorrectByQuestionIdAsync(int questionId)
    {
        var question = await _applicationContext.Questions.FirstOrDefaultAsync(q => q.Id == questionId);
        if (question is null)
        {
            return null;
        }

        var answers = await GetAllByQuestionIdAsync(questionId);
        var correct = answers?.SingleOrDefault(a => a.IsCorrect);
       
        return correct;
    }
    public async Task<IEnumerable<Answer>?> GetAllByQuestionIdAsync(int questionId) => await _applicationContext
        .Answers
        .Where(a => a.QuestionId == questionId)
        .ToArrayAsync();
    public async Task<Answer?> GetByIdAsync(int id) => await _applicationContext
        .Answers
        .Include(a => a.Question!)
        .FirstOrDefaultAsync(a => a.Id == id);
    public async Task<Answer> CreateAsync(Answer answer)
    {
        ArgumentNullException.ThrowIfNull(answer);

        await _applicationContext.Answers.AddAsync(answer);
        await _applicationContext.SaveChangesAsync();
        
        return answer;
    }
    public async Task DeleteAsync(int id)
    {
        var answer = await _applicationContext.Answers.FirstOrDefaultAsync(a => a.Id == id);
        if (answer is null)
        {
            return;
        }

        _applicationContext.Answers.Remove(answer);
        await _applicationContext.SaveChangesAsync();
    }
    public async Task UpdateAsync(Answer answer)
    {
        ArgumentNullException.ThrowIfNull(answer);

        var exist = await _applicationContext.Answers.FirstOrDefaultAsync(a => a.Id == answer.Id)
            ?? throw new Exception($"Cannot find answer with id == {answer.Id}");

        exist.QuestionId = answer.QuestionId;
        exist.Text = answer.Text;

        await _applicationContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Answer>?> GetAllByTestIdAsync(int testId) => await _applicationContext
        .Answers
        .Include(a => a.Question!)
        .Where(a => a.Question!.TestId == testId)
        .ToArrayAsync();
    public async Task<IEnumerable<Answer>?> GetCorrectByTestIdAsync(int testId) => await _applicationContext
        .Answers
        .Include(a => a.Question!)
        .Where(a => a.Question!.TestId == testId)
        .ToArrayAsync();
}
