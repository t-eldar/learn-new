using LearnNew.Models.Core;

namespace LearnNew.Core.Repositories.Interfaces;
public interface ICourseRepository
{
    Task<IEnumerable<Course>?> GetAllAsync();
    Task<IEnumerable<Course>?> GetByTeacherIdAsync(int teacherId);
    Task<Course?> GetByIdAsync(int id);
    Task<Course> CreateAsync(Course course);
    Task DeleteAsync(int id);
    Task UpdateAsync(Course course);
}
