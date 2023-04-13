using LearnNew.Models.Authentication;

namespace LearnNew.Models.Core;
public class Test
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required int LessonId { get; set; }
    public Lesson? Lesson { get; set; }
    public required int CreatorId { get; set; }
    public User? Creator { get; set; }
    public IList<Question>? Questions { get; set; }
}
