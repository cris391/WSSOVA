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
    public DbSet<Annotation> Annotations { get; set; }
    public DbSet<AnnotationFunction> AnnotationFunction { get; set; }

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
      // modelBuilder.Entity<Annotation>().hasF

      // modelBuilder.Entity<Question>().HasOne(pt => pt.Answer)
      //       .WithOne(p => p.Question)
      //       .HasForeignKey<Answer>(pt => pt.QuestionForeignKey);
      // modelBuilder.Entity<Question>().ToTable("questions");
      // modelBuilder.Entity<Question>().Property(m => m.Id).HasColumnName("questionid");
      // modelBuilder.Entity<Question>().Property(m => m.ClosedDate).HasColumnName("closeddate");


      // modelBuilder.Entity<OrderDetails>().ToTable("orderdetails");
      // modelBuilder.Entity<OrderDetails>().Property(m => m.OrderId).HasColumnName("orderid");
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
