using LearnNew.Core.DatabaseContext;
using LearnNew.Core.Repositories.Interfaces;
using LearnNew.Models.Core;

using Microsoft.EntityFrameworkCore;

namespace LearnNew.Core.Repositories.Implementations;
public class QuestionRepository : IQuestionRepository
{
    private readonly ApplicationContext _applicationContext;

    public QuestionRepository(ApplicationContext applicationContext) => _applicationContext = applicationContext;
    public async Task<Question?> GetByIdAsync(int id) => await _applicationContext
        .Questions
        .Include(q => q.Answers)
        .FirstOrDefaultAsync(q => q.Id == id);
    public async Task<IEnumerable<Question>?> GetByTestIdAsync(int testId) => await _applicationContext
        .Questions
        .Include(q => q.Answers)
        .Where(q => q.TestId == testId)
        .ToArrayAsync();

    public async Task<Question> CreateAsync(Question question)
    {
        ArgumentNullException.ThrowIfNull(question);

        await _applicationContext.Questions.AddAsync(question);
        await _applicationContext.SaveChangesAsync();

        return question;
    }
    public async Task DeleteAsync(int id)
    {
        var question = await _applicationContext.Questions.FirstOrDefaultAsync(q => q.Id == id);
        if (question is null)
        {
            return;
        }

        _applicationContext.Questions.Remove(question);
        await _applicationContext.SaveChangesAsync();
    }
    public async Task UpdateAsync(Question question)
    {
        ArgumentNullException.ThrowIfNull(question);

        var exist = await _applicationContext.Questions.FirstOrDefaultAsync(q => q.Id == question.Id)
            ?? throw new Exception($"Cannot find question with id == {question.Id}");

        exist.TestId = question.TestId;
        exist.Content = question.Content;

        await _applicationContext.SaveChangesAsync();
    }
}
