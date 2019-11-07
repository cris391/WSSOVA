using System;
using Microsoft.EntityFrameworkCore;
using DatabaseService;


namespace DatabaseService
{
  public class SOContext : DbContext
  {
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Annotation> Annotation { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseNpgsql(connectionString: "host=localhost;db=stack_overflow;uid=postgres;pwd=");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Question>().ToTable("questions");
      modelBuilder.Entity<Question>().Property(m => m.QuestionId).HasColumnName("questionid");
      modelBuilder.Entity<Question>().Property(m => m.ClosedDate).HasColumnName("closeddate");
      modelBuilder.Entity<Question>().Property(m => m.Title).HasColumnName("title");
      modelBuilder.Entity<Question>().Property(m => m.AcceptedAnswerId).HasColumnName("acceptedanswerid");
      modelBuilder.Entity<Question>().Property(m => m.Postid).HasColumnName("postid"); 


      modelBuilder.Entity<Answer>().ToTable("answers");
      modelBuilder.Entity<Answer>().Property(m => m.Id).HasColumnName("answerid");
      modelBuilder.Entity<Answer>().Property(m => m.parentId).HasColumnName("parentid");
      modelBuilder.Entity<Answer>().Property(m => m.PostId).HasColumnName("postid");


      modelBuilder.Entity<Post>().ToTable("posts");
      modelBuilder.Entity<Post>().Property(m => m.Id).HasColumnName("postid");
      modelBuilder.Entity<Post>().Property(m => m.CreationDate).HasColumnName("creationdate");
      modelBuilder.Entity<Post>().Property(m => m.Score).HasColumnName("score");
      modelBuilder.Entity<Post>().Property(m => m.Body).HasColumnName("body");

     



      /*
      modelBuilder.Entity<Annotation>().ToTable("annotation");
      modelBuilder.Entity<Annotation>().Property("userId");
      modelBuilder.Entity<Annotation>().Property("questionid");
      modelBuilder.Entity<Annotation>().Property("body");
      */


    }
  }
}
