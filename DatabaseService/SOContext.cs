using System;
using Microsoft.EntityFrameworkCore;
using DatabaseService;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace DatabaseService
{

  public class SOContext : DbContext
  {
    public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Annotation> Annotations { get; set; }
    public DbSet<Marking> Markings { get; set; }
    public DbSet<AnnotationFunction> AnnotationFunction { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<SearchResult> SearchResults { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Owner> Owners { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

      optionsBuilder
      .UseLoggerFactory(MyLoggerFactory)
      .UseNpgsql(connectionString: "host=localhost;db=stackoverflow;uid=postgres;pwd=root");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.CreateMap("Id, Name");
      modelBuilder.Entity<AnnotationFunction>().HasNoKey();
      modelBuilder.Entity<SearchResult>().HasNoKey();
      modelBuilder.Entity<Tag>().HasNoKey();
      modelBuilder.Entity<User>().ToTable("app_users");;
      modelBuilder.Entity<Owner>().ToTable("stack_users");;
      // modelBuilder.Entity<Owner>().HasKey("userid");
      // modelBuilder.Entity<Owner>().Property(o => o.UserId).HasColumnName("description");
    }
  }

  static class ModelBuilderExtensions
  {
    public static void CreateMap(
        this ModelBuilder modelBuilder,
        params string[] names)
    {
      foreach (var entityType in modelBuilder.Model.GetEntityTypes())
      {
        var dict = new List<string>(names);
        entityType.SetTableName(entityType.GetTableName().ToLower());
        foreach (var property in entityType.GetProperties())
        {
          var propertyName = property.Name.ToLower();
          // var entityName = "";

          // if (dict.Contains(property.Name))
          // {
          //   entityName = entityType.ClrType.Name.ToLower();
          // }

          property.SetColumnName(propertyName);
        }

      }
    }
  }
}
