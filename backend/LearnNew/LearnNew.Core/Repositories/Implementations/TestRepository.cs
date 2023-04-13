using LearnNew.Core.DatabaseContext;
using LearnNew.Core.Repositories.Interfaces;
using LearnNew.Models.Core;

using Microsoft.EntityFrameworkCore;

namespace LearnNew.Core.Repositories.Implementations;
public class TestRepository : ITestRepository
{
    private readonly ApplicationContext _applicationContext;

    public TestRepository(ApplicationContext applicationContext) => _applicationContext = applicationContext;
    public async Task<IEnumerable<Test>?> GetAllAsync() => await _applicationContext
        .Tests.ToArrayAsync();
    public async Task<IEnumerable<Test>?> GetByLessonIdAsync(int lessonId) => await _applicationContext
        .Tests
        .Include(t => t.Questions!)
            .ThenInclude(q => q.Answers)
        .Where(t => t.LessonId == lessonId)
        .ToArrayAsync();
    public async Task<Test?> GetByIdAsync(int id) => await _applicationContext
        .Tests.FirstOrDefaultAsync(t => t.Id == id);

    public async Task<Test> CreateAsync(Test test)
    {
        ArgumentNullException.ThrowIfNull(test);

        await _applicationContext.Tests.AddAsync(test);
        await _applicationContext.SaveChangesAsync();

        return test;
    }
    public async Task DeleteAsync(int id)
    {
        var test = await _applicationContext.Tests.FirstOrDefaultAsync(t => t.Id == id);
        if (test is null)
        {
            return;
        }

        _applicationContext.Tests.Remove(test);
        await _applicationContext.SaveChangesAsync();
    }
    public async Task UpdateAsync(Test test)
    {
        ArgumentNullException.ThrowIfNull(test);

        var exist = await _applicationContext.Tests.FirstOrDefaultAsync(t => t.Id == test.Id)
           ?? throw new Exception($"Cannot find test with id == {test.Id}");

        exist.Title = test.Title;
        exist.LessonId = test.LessonId;

        await _applicationContext.SaveChangesAsync();
    }
}
