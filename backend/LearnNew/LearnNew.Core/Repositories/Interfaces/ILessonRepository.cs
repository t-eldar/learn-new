using LearnNew.Models.Core;

namespace LearnNew.Core.Repositories.Interfaces;
public interface ILessonRepository
{
    Task<IEnumerable<Lesson>?> GetAllAsync();
    Task<IEnumerable<Lesson>?> GetByCourseIdAsync(int courseId);
    Task<Lesson?> GetByIdAsync(int id);
    Task<Lesson> CreateAsync(Lesson lesson);
    Task DeleteAsync(int id);
    Task UpdateAsync(Lesson lesson);
}
