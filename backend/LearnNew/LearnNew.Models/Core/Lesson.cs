using LearnNew.Models.Authentication;

namespace LearnNew.Models.Core;
public class Lesson
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required int CourseId { get; set; }
    public Course? Course { get; set; } 
    public required int CreatorId { get; set; }
    public User? Creator { get; set; }

    public IEnumerable<Test>? Tests { get; set; }
}
