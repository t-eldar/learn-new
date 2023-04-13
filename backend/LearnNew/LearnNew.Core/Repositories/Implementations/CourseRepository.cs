using LearnNew.Core.DatabaseContext;
using LearnNew.Core.Repositories.Interfaces;
using LearnNew.Models.Core;

using Microsoft.EntityFrameworkCore;

namespace LearnNew.Core.Repositories.Implementations;
public class CourseRepository : ICourseRepository
{
    private readonly ApplicationContext _applicationContext;
    public CourseRepository(ApplicationContext applicationContext) => _applicationContext = applicationContext;

    public async Task<IEnumerable<Course>?> GetAllAsync() => await _applicationContext.Courses.ToArrayAsync();
    public async Task<Course?> GetByIdAsync(int id) => await _applicationContext.Courses.FirstOrDefaultAsync(c => c.Id == id);
    public async Task<IEnumerable<Course>?> GetByTeacherIdAsync(int teacherId) => await _applicationContext
        .Courses
        .Where(c => c.TeacherId == teacherId)
        .ToArrayAsync();
    public async Task<Course> CreateAsync(Course course)
    {
        ArgumentNullException.ThrowIfNull(course);

        await _applicationContext.Courses.AddAsync(course);
        await _applicationContext.SaveChangesAsync();

        return course;
    }
    public async Task DeleteAsync(int id)
    {
        var course = await _applicationContext.Courses.FirstOrDefaultAsync(c => c.Id == id);
        if (course is null)
        {
            return;
        }

        _applicationContext.Courses.Remove(course);
        await _applicationContext.SaveChangesAsync();
    }
    public async Task UpdateAsync(Course course)
    {
        ArgumentNullException.ThrowIfNull(course);

        var exist = await _applicationContext.Courses.FirstOrDefaultAsync(c => c.Id == course.Id)
            ?? throw new Exception($"Cannot find course with id == {course.Id}");

        exist.TeacherId = course.TeacherId;
        exist.Name = course.Name;
        exist.Description = course.Description;

        await _applicationContext.SaveChangesAsync();
    }
}
