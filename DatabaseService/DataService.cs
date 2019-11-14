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
          .FromSqlRaw("select addAnnotation({0}, {1}) as Id", annotation.MarkingId, annotation.Body)
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

    public List<Annotation> GetAnnotations(int markingId)
    {
      using var db = new SOContext();
      var result = new List<Annotation>();

      return db.Annotations.Where(a => a.MarkingId == markingId).ToList();
    }

    public bool CreateMarking(Marking marking)
    {
      using var db = new SOContext();

      try
      {
        db.Markings.Add(marking);
        var hei = db.SaveChanges();

        return true;
      }
      catch (System.Exception e)
      {
        return false;
      }
    }
    public List<Marking> GetMarkings(int userid)
    {
      using var db = new SOContext();

      var markings = db.Markings
        .Where(m => m.UserId == userid)
        .ToList();

      return markings;
    }

    public bool DeleteMarking(Marking marking)
    {
      using var db = new SOContext();
      try
      {
        db.Markings.Remove(marking);
        db.SaveChanges();
        return true;
      }
      catch (System.Exception e)
      {
        return false;
      }
    }
    public bool DeleteAnnotation(Annotation annotation)
    {
      using var db = new SOContext();
      try
      {
        db.Annotations.Remove(annotation);
        db.SaveChanges();
        return true;
      }
      catch (System.Exception e)
      {
        return false;
      }
    }
    public User GetUser(string username)
     {
        using var db = new SOContext();
        return db.User.FirstOrDefault(x => x.Username == username);
     }
          public User CreateUser(string username, string password, string salt)
     {
            using var db = new SOContext();
            var user = new User()
            {
               
                Username = username,
                Password = password,
                Salt = salt      
            };
            try {
                db.User.Add(user);
                db.SaveChanges();
                Console.WriteLine("@@@@@@@@@@@@@ stored in db @@@@@@@@@@@@@@@@");
                return user;

            } catch( Exception e)
            {
                Console.WriteLine("@@@@@@@@@@@@@ failed stored in db @@@@@@@@@@@@@@@@");
                Console.Write(e);
                return user;
            }   
     }
     public List<Post> GetAuthPosts(int userId)
        {
            var db = new SOContext();
            Console.WriteLine("@@@@@@@@@@@@@ USER ID @@@@@@@@@@@@@@@@");
            Console.WriteLine(@"User id = {0} ", userId);
            if (db.User.FirstOrDefault(x => x.Id == userId) == null)
                throw new ArgumentException("user not found");
            return _posts;
        }
  }
}