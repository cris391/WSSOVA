using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseService;
using Microsoft.EntityFrameworkCore;

namespace DatabaseService
{

  public class DataService : IDataService
  {
    public List<Question> GetQuestions(PagingAttributes pagingAttributes)
    {
      using var db = new SOContext();
      return db.Questions
                .Include(q => q.Post)
                .Skip(pagingAttributes.Page * pagingAttributes.PageSize)
                .Take(pagingAttributes.PageSize)
                .ToList();
    }
    public void GetQuestionWithAnswers(int questionId)
    {
      using var db = new SOContext();
      var question = db.Questions.Find(questionId);
      var answers = db.Answers
        .Where(p => p.ParentId == questionId)
        .ToList();
      Console.WriteLine("@@@@@@@@@@@@");
      Console.WriteLine(answers.Count);
    }

    public int NumberOfQuestions()
    {
      using var db = new SOContext();
      return db.Questions.Count();
    }

    public Question GetQuestion(int id)
    {
      using var db = new SOContext();
      return db.Questions.Find(id);
    }

    public List<Post> GetPosts()
    {
      using var db = new SOContext();

      return db.Posts
        // .Include(p => p.Question)
        .Take(10).ToList();
    }

    // public Category CreateCategory(string name, string description)
    // {
    //   using var db = new NorthwindContext();
    //   var nextId = db.Categories.Max(x => x.Id) + 1;
    //   var category = new Category { Id = nextId, Name = name, Description = description };
    //   db.Categories.Add(category);
    //   db.SaveChanges();

    //   return GetCategory(category.Id);
    // }


    // public bool DeleteCategory(int id)
    // {
    //   using var db = new NorthwindContext();
    //   var category = GetCategory(id);
    //   try
    //   {
    //     db.Categories.Remove(category);
    //     db.SaveChanges();
    //   }
    //   catch (System.Exception e)
    //   {
    //     return false;
    //   }
    //   return true;
    // }

    // public bool UpdateCategory(int id, string name, string description)
    // {
    //   try
    //   {
    //     using var db = new NorthwindContext();
    //     var category = GetCategory(id);
    //     category.Name = name;
    //     category.Description = description;
    //     db.Update(category);
    //     db.SaveChanges();
    //   }
    //   catch (System.Exception e)
    //   {
    //     return false;
    //   }
    //   return true;
    // }
    // public bool PutCategory(int id, string name, string description)
    // {
    //   try
    //   {
    //     using var db = new NorthwindContext();
    //     var category = GetCategory(id);
    //     category.Name = name;
    //     category.Description = description;
    //     db.Update(category); 
    //     db.SaveChanges();
    //   }
    //   catch (System.Exception e)
    //   {
    //     return false;
    //   }
    //   return true;
    // }

    // public Product GetProduct(int id)
    // {
    //   using var db = new NorthwindContext();
    //   return db.Products
    //     .Where(p => p.Id == id)
    //     .Include(p => p.Category)
    //     .FirstOrDefault();
    // }

    // public List<Product> GetProductByCategory(int id)
    // {
    //   using var db = new NorthwindContext();
    //   return db.Products
    //     .Where(p => p.CategoryId == id)
    //     .Include(p => p.Category)
    //     .ToList();
    // }

    // public List<Product> GetProductByName(string name)
    // {
    //   using var db = new NorthwindContext();
    //   return db.Products
    //     .Where(p => p.Name.Contains(name))
    //     .ToList();
    // }

    // public Order GetOrder(int id)
    // {
    //   using var db = new NorthwindContext();
    //   return db.Orders
    //     .Where(p => p.Id == id)
    //     .Include(p => p.OrderDetails)
    //     .ThenInclude(p => p.Product)
    //     .ThenInclude(p => p.Category)
    //     .FirstOrDefault();
    // }

    // public List<Order> GetOrders()
    // {
    //   using var db = new NorthwindContext();
    //   return db.Orders.ToList();
    // }

    // public List<OrderDetails> GetOrderDetailsByOrderId(int id)
    // {
    //   using var db = new NorthwindContext();
    //   return db.OrderDetails
    //   .Where(m => m.OrderId == id)
    //   .Include(m => m.Product)
    //   .ToList();
    // }

    // public List<OrderDetails> GetOrderDetailsByProductId(int id)
    // {
    //   using var db = new NorthwindContext();
    //   return db.OrderDetails
    //   .Where(m => m.ProductId == id)
    //   .Include(m => m.Order)
    //   .ToList();
    // }
  }
}