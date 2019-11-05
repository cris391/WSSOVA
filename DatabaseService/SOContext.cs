using System;
using Microsoft.EntityFrameworkCore;
using DatabaseService;


namespace DatabaseService
{
  public class SOContext : DbContext
  {
    public DbSet<Question> Questions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseNpgsql(connectionString: "host=localhost;db=stackoverflow;uid=postgres;pwd=root");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Question>().ToTable("questions");
      modelBuilder.Entity<Question>().Property(m => m.Id).HasColumnName("questionid");
      modelBuilder.Entity<Question>().Property(m => m.ClosedDate).HasColumnName("closeddate");
      modelBuilder.Entity<Question>().Property(m => m.Title).HasColumnName("title");

      // modelBuilder.Entity<OrderDetails>().ToTable("orderdetails");
      // modelBuilder.Entity<OrderDetails>().Property(m => m.OrderId).HasColumnName("orderid");
      // modelBuilder.Entity<OrderDetails>().Property(m => m.ProductId).HasColumnName("productid");
      // modelBuilder.Entity<OrderDetails>().Property(m => m.UnitPrice).HasColumnName("unitprice");
      // modelBuilder.Entity<OrderDetails>().Property(m => m.Quantity).HasColumnName("quantity");
      // modelBuilder.Entity<OrderDetails>().Property(m => m.Discount).HasColumnName("discount");
      // modelBuilder.Entity<OrderDetails>().HasKey(m => new { m.OrderId, m.ProductId });
    }
  }
}
