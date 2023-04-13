using LearnNew.Models.Core;

namespace LearnNew.Core.Repositories.Interfaces;
public interface ITestRepository
{
    Task<IEnumerable<Test>?> GetAllAsync();
    Task<IEnumerable<Test>?> GetByLessonIdAsync(int lessonId);
    Task<Test?> GetByIdAsync(int id);
    Task<Test> CreateAsync(Test test);
    Task DeleteAsync(int id);
    Task UpdateAsync(Test test);
}
