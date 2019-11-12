using System;
using Microsoft.EntityFrameworkCore;
using DatabaseService;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseService
{
    public class SaveUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }


    public class SOContext : DbContext
  {
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Annotation> Annotation { get; set; }
    public DbSet<User> User { get; set; }

    public DbQuery<User> Create_User { get; set; }

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
      modelBuilder.Entity<Annotation>().Property(m => m.UserId).HasColumnName("userid");
      modelBuilder.Entity<Annotation>().Property(m => m.QuestionId).HasColumnName("questionid");
      modelBuilder.Entity<Annotation>().Property(m => m.Body).HasColumnName("body");

      modelBuilder.Entity<User>().ToTable("app_users");
      modelBuilder.Entity<User>().Property(m => m.Id).HasColumnName("userid");
      modelBuilder.Entity<User>().Property(m => m.Username).HasColumnName("username");
      modelBuilder.Entity<User>().Property(m => m.Password).HasColumnName("password");
      modelBuilder.Entity<User>().Property(m => m.Salt).HasColumnName("salt");
      modelBuilder.Entity<User>().Property(m => m.Name).HasColumnName("name");

     modelBuilder.Query<SaveUser>().Property(x => x.Id).HasColumnName("userid");
     modelBuilder.Query<SaveUser>().Property(m => m.Username).HasColumnName("username");
     modelBuilder.Query<SaveUser>().Property(m => m.Password).HasColumnName("password");
     modelBuilder.Query<SaveUser>().Property(m => m.Salt).HasColumnName("salt");
     modelBuilder.Query<SaveUser>().Property(m => m.Name).HasColumnName("name");

        }
  }
}
