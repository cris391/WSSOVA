using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseService;
using Microsoft.EntityFrameworkCore;

namespace DatabaseService
{

  public class DataService: IDataService
  {

    readonly List<User> _users = new List<User>();
        // auth
    readonly List<Post> _posts = new List<Post>();

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
      var questionDto = Helpers.CreateQuestionDtos(question);
      return questionDto;
    }

     public List<Annotation> GetAnnotations(int userId)
     {
        using var db = new SOContext();
        var userAnnotations = db.Annotation.Where(x => x.UserId == userId); 
        return userAnnotations.ToList();
     }

     public Annotation GetAnnotation(int userId, int questionId)
      {
         using var db = new SOContext();
         var userAnnotation = db.Annotation.Where(x => x.UserId == userId && x.QuestionId == questionId);

         return userAnnotation.First();
      }

      public Annotation CreateAnnotation(int userId, int questionId, string body)
      {
         using var db = new SOContext();
         var annotation = new Annotation { UserId = userId, QuestionId = questionId, Body = body };
         db.Annotation.Add(annotation);
         db.SaveChanges();
         return GetAnnotation(annotation.UserId, annotation.QuestionId);
      }

      public bool DeleteAnnotation(int userId, int questionId)
      {
        using var db = new SOContext();
        var annotation = GetAnnotation(userId, questionId);
        try
          {
              db.Annotation.Remove(annotation);
              db.SaveChanges();
          } catch (System.Exception)
            {
                return false; 
            }
            return db.SaveChanges() > 0;
      }

      public bool UpdateAnnotation(int userId, int questionId, string body)
      {
        try {
           using var db = new SOContext();
           var annotation = GetAnnotation(userId, questionId);
           annotation.Body = body;
           db.Update(annotation);
           db.SaveChanges();

         } catch (System.Exception)
            {
                return false;
            }
         return true;
      }

    /* Instantiate 1 test user delete on reload */
    public DataService()
    {
            _users.Add(new User()
            {
                Id = 999999,
                Username = "klomanden"
            });
    }


     public User GetUser(string username)
     {
        return _users.FirstOrDefault(x => x.Username == username);
     }


     public User CreateUser(string username, string password, string salt)
     {
            var user = new User()
            {
                Id = _users.Max(x => x.Id) + 1,
                Username = username,
                Password = password,
                Salt = salt
            };
            _users.Add(user);
            Console.Write("@@@@@@@@@@@@@ NEWUSER @@@@@@@@@@@@@@@@");
            Console.WriteLine(user);
            return user;
     }

    public List<Post> GetAuthPosts(int userId)
        {
            if (_users.FirstOrDefault(x => x.Id == userId) == null)
                throw new ArgumentException("user not found");
            return _posts;
        }






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