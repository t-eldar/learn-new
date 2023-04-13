using LearnNew.Models.Authentication;

namespace LearnNew.Models.Core;
public class Course
{
    public int Id { get; set; }
    public string? CoverImageUrl { get; set; }
    public required int CreatorId { get; set; }
    public User? Creator { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required DateTime DateCreated { get; set; }

    public IEnumerable<Lesson>? Lessons { get; set; }
}
