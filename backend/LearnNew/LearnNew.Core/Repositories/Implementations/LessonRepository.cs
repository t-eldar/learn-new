using LearnNew.Core.DatabaseContext;
using LearnNew.Core.Repositories.Interfaces;
using LearnNew.Models.Core;

using Microsoft.EntityFrameworkCore;

namespace LearnNew.Core.Repositories.Implementations;
public class LessonRepository : ILessonRepository
{
    private readonly ApplicationContext _applicationContext;

    public LessonRepository(ApplicationContext applicationContext) => _applicationContext = applicationContext;
    public async Task<Lesson?> GetByIdAsync(int id) => await _applicationContext
        .Lessons
        .FirstOrDefaultAsync(l => l.Id == id);
    public async Task<IEnumerable<Lesson>?> GetAllAsync() => await _applicationContext.Lessons.ToArrayAsync();
    public async Task<IEnumerable<Lesson>?> GetByCourseIdAsync(int courseId) => await _applicationContext
        .Lessons
        .Where(l => l.CourseId == courseId)
        .ToArrayAsync();

    public async Task<Lesson> CreateAsync(Lesson lesson)
    {
        ArgumentNullException.ThrowIfNull(lesson);

        await _applicationContext.Lessons.AddAsync(lesson);
        await _applicationContext.SaveChangesAsync();

        return lesson;
    }
    public async Task DeleteAsync(int id)
    {
        var lesson = await _applicationContext.Lessons.FirstOrDefaultAsync(l => l.Id == id);
        if (lesson is null)
        {
            return;
        }

        _applicationContext.Lessons.Remove(lesson);
        await _applicationContext.SaveChangesAsync();
    }
    public async Task UpdateAsync(Lesson lesson)
    {
        ArgumentNullException.ThrowIfNull(lesson);

        var exist = await _applicationContext.Lessons.FirstOrDefaultAsync(c => c.Id == lesson.Id)
            ?? throw new Exception($"Cannot find lesson with id == {lesson.Id}");

        exist.CourseId = lesson.CourseId;
        exist.Title = lesson.Title;
        exist.Content = lesson.Content;

        await _applicationContext.SaveChangesAsync();
    }
}
