using System;
using Microsoft.EntityFrameworkCore;
using DatabaseService;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace DatabaseService
{
  //public class SaveUser
  //{
  //    public int Id { get; set; }
  //    public string Name { get; set; }
  //    public string Username { get; set; }
  //    public string Password { get; set; }
  //    public string Salt { get; set; }
  //}


  public class SOContext : DbContext
  {
    public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Annotation> Annotations { get; set; }
    public DbSet<Marking> Markings { get; set; }
    public DbSet<AnnotationFunction> AnnotationFunction { get; set; }
    public DbSet<User> Users { get; set; }

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
      modelBuilder.Entity<User>().ToTable("app_users");;
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
