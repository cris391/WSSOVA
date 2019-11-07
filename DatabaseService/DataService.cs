using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseService;
using Microsoft.EntityFrameworkCore;

namespace DatabaseService
{

  public class DataService: IDataService
  {
    public List<Question> GetQuestions()
    {
      using var db = new SOContext();
      return db.Questions.ToList();
    }

    public Question GetQuestion(int id)
    {
      using var db = new SOContext();
      return db.Questions.Find(id);
    }

    public List<Answer> GetAnswers()
    {
      using var db = new SOContext();
      return db.Answers.Include(p => p.PostId).ToList(); 
    }

    public Answer GetAnswer(int id)
    {
      using var db = new SOContext();
      return db.Answers.Find(id); 
    }

    public List<Post> GetPosts()
    {
      using var db = new SOContext();
      return db.Posts.ToList(); 
    }

    public Post GetPost(int id)
    {
      using var db = new SOContext();
      return db.Posts.Find(id);
    }

    public QuestionDto GetFullQuestion (int questionId)
    {
      using var db = new SOContext();
      var question = db.Questions.Where(x => x.QuestionId == questionId).Include(x => x.Post).FirstOrDefault();
      /*
            .Where(q => q.QuestionId == questionId)
            .FirstOrDefault();
       */
      var questionDto = Helpers.CreateQuestionDtos(question);
      return questionDto;
    }


            /*
       public Annotation GetAnnotation(int id)
       {
          using var db = new SOContext();
          return db.Annotation.Find(id);
       }
       */

        /*
        public Annotation CreateAnnotation(string name, string body)
        {
          using var db = new SOContext();
          var nextId = db.Annotation.Max(x => x.Id) + 1;
          var annotation = new Annotation { Id = nextId, Body = body };
          db.Annotation.Add(annotation);
          db.SaveChanges();

          return GetAnnotation(annotation.Id);
         }
         */

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