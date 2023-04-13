using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using LearnNew.Models.Core;

namespace LearnNew.Models.Authentication;
public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }

    [JsonIgnore]
    [Required]
    public string PasswordHash { get; set; } = null!;

    public IEnumerable<TestScore>? Scores { get; set; }
    public IEnumerable<Course>? Courses { get; set; }
}
