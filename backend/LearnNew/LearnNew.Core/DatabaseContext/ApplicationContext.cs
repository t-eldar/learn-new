using LearnNew.Models.Authentication;
using LearnNew.Models.Core;

using Microsoft.EntityFrameworkCore;

namespace LearnNew.Core.DatabaseContext;
public class ApplicationContext : DbContext
{
    public DbSet<TestScore> TestScores { get; set; }
    public DbSet<QuestionScore> QuestionScores { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Test> Tests { get; set; }

    public ApplicationContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TestScore>(entity =>
        {
            entity.HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(s => s.Test)
                .WithMany()
                .HasForeignKey(s => s.TestId)
                .OnDelete(DeleteBehavior.NoAction);
        });
        modelBuilder.Entity<QuestionScore>(entity =>
        {
            entity.HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(s => s.TestScore)
                .WithMany()
                .HasForeignKey(s => s.TestScoreId)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }
}
