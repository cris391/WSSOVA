using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseService;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DatabaseService
{

  public class DataService : IDataService
  {
    public List<Question> GetQuestions(PagingAttributes pagingAttributes)
    {
      using var db = new SOContext();
      var questions = db.Questions
                .Include(q => q.Post)
                .Skip(pagingAttributes.Page * pagingAttributes.PageSize)
                .Take(pagingAttributes.PageSize)
                .ToList();


      return questions;
    }
    public object GetQuestionWithAnswers(int questionId)
    {
      using var db = new SOContext();
      var question = db.Questions
        .Where(q => q.QuestionId == questionId)
        .Include(q => q.Post).FirstOrDefault();

      // var answers = db.Answers
      //   .Where(a => a.PostId == questionId)
      //   .Include(a => a.Post) // this gets body of post of type question(and not of type answer)
      //   .ToList();

      // var fullAnswers = new List<object>();
      // foreach (var answer in answers)
      // {
      //   Console.WriteLine(answer.AnswerId);
      //   var post = db.Posts.Find(answer.AnswerId);
      //   fullAnswers.Add(post);
      // }

      var answers = (from a in db.Answers
                     join p in db.Posts
                     on a.AnswerId equals p.PostId
                     where a.PostId == questionId
                     select new
                     {
                       Id = p.PostId,
                       p.CreationDate,
                       p.Score,
                       p.Body
                     }).ToList();

      // var answerDtos = Helpers.CreateAnswerDtos(answers);
      return new { Question = question, Answers = answers };
    }

    public int NumberOfQuestions()
    {
      using var db = new SOContext();
      return db.Questions.Count();
    }

    public Question GetQuestion(int id)
    {
      using var db = new SOContext();
      return db.Questions
                .Where(q => q.QuestionId == id)
                .Include(q => q.Post)
                .FirstOrDefault();
    }

    public List<Post> GetPosts()
    {
      using var db = new SOContext();

      return db.Posts
        // .Include(p => p.Question)
        .Take(10).ToList();
    }

    public AnswerDbDto GetAnswer(int answerId)
    {
      using var db = new SOContext();

      var answers = (from a in db.Answers
                     join p in db.Posts
                     on a.AnswerId equals p.PostId
                     where a.AnswerId == answerId
                     select new
                     AnswerDbDto
                     {
                       AnswerId = p.PostId,
                       ParentId = a.PostId,
                       CreationDate = p.CreationDate,
                       Score = p.Score,
                       Body = p.Body
                     }).FirstOrDefault();
      Console.WriteLine("@@@@@@@@@@@@@@@@");
      Console.WriteLine(answers);
      return answers;
    }

    public List<AnswerDbDto> GetAnswersForQuestion(int questionId)
    {
      using var db = new SOContext();
      var answers = (from a in db.Answers
                     join p in db.Posts
                     on a.AnswerId equals p.PostId
                     where a.PostId == questionId
                     select new
                     AnswerDbDto
                     {
                       AnswerId = p.PostId,
                       ParentId = a.PostId,
                       CreationDate = p.CreationDate,
                       Score = p.Score,
                       Body = p.Body
                     }).ToList();

      return answers;
    }

    public int AddAnnotation(Annotation annotation)
    {
      using var db = new SOContext();

      // you can add parameters to the query, as shown here, by list them after the 
      // statement, and reference them with {0} {1} ... {n}, where 0 is the first argument,
      // 1 is the second etc.
      try
      {
        return db.AnnotationFunction
          .FromSqlRaw("select addAnnotation({0}, {1}, {2}) as Id", annotation.UserId, annotation.QuestionId, annotation.Body)
          .FirstOrDefault()
          .Id;
      }
      catch (System.Exception e)
      {

        return 0;
      }
    }

    public Annotation GetAnnotation(int annotationId)
    {
      using var db = new SOContext();

      var annotation = db.Annotations.Find(annotationId);
      return annotation;
    }

    public bool UpdateAnnotation(Annotation annotation)
    {
      using var db = new SOContext();

      try
      {
        var oldAnnotation = db.Annotations.Find(annotation.AnnotationId);
        oldAnnotation.Body = annotation.Body;
        db.Annotations.Update(oldAnnotation);
        db.SaveChanges();

        return true;
      }
      catch (System.Exception)
      {
        return false;
      }
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