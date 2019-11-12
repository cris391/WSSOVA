using System;
using Microsoft.EntityFrameworkCore;
using DatabaseService;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseService
{
    public class SOContext : DbContext
  {
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Annotation> Annotation { get; set; }
    public DbSet<User> User { get; set; }

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

  
      modelBuilder.Entity<Annotation>().ToTable("annotations");
      modelBuilder.Entity<Annotation>().Property(m => m.Id).HasColumnName("id");
      modelBuilder.Entity<Annotation>().Property(m => m.UserId).HasColumnName("userid");
      modelBuilder.Entity<Annotation>().Property(m => m.QuestionId).HasColumnName("questionid");
      modelBuilder.Entity<Annotation>().Property(m => m.Body).HasColumnName("body");

      modelBuilder.Entity<User>().ToTable("app_users");
      modelBuilder.Entity<User>().Property(m => m.Id).HasColumnName("userid");
      modelBuilder.Entity<User>().Property(m => m.Username).HasColumnName("username");
      modelBuilder.Entity<User>().Property(m => m.Password).HasColumnName("password");
      modelBuilder.Entity<User>().Property(m => m.Salt).HasColumnName("salt");

      modelBuilder.Entity<Comment>().ToTable("comments");
      modelBuilder.Entity<Comment>().Property(m => m.Id).HasColumnName("commentid");
      modelBuilder.Entity<Comment>().Property(m => m.UserId).HasColumnName("userid");
      modelBuilder.Entity<Comment>().Property(m => m.PostId).HasColumnName("postid");
      modelBuilder.Entity<Comment>().Property(m => m.CommentScore).HasColumnName("commentscore");
      modelBuilder.Entity<Comment>().Property(m => m.CommentText).HasColumnName("commenttext");
      modelBuilder.Entity<Comment>().Property(m => m.Timestamp).HasColumnName("commentcreatedate");

      modelBuilder.Entity<Tag>().ToTable("tags");
      modelBuilder.Entity<Tag>().Property(m => m.TagId).HasColumnName("tagid");
      modelBuilder.Entity<Tag>().Property(m => m.Value).HasColumnName("value");
    }
  }
}
