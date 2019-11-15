using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

    public QuestionDbDto GetQuestion(int id)
    {
      using var db = new SOContext();

      var questionDto = (from q in db.Questions
                         join p in db.Posts
                         on q.QuestionId equals p.PostId
                         where q.QuestionId == id
                         select new QuestionDbDto
                         {
                           Title = q.Title,
                           QuestionId = p.PostId,
                           CreationDate = p.CreationDate,
                           ClosedDate = q.ClosedDate,
                           Score = p.Score,
                           Body = p.Body
                         }).FirstOrDefault();
      return questionDto;
    }

    public List<Post> GetPosts()
    {
      using var db = new SOContext();

      return db.Posts
        // .Include(p => p.Question)
        .Take(10).ToList();
    }

    public FullPost GetFullPost(int questionId)
    {
      using var db = new SOContext();

      var question = GetQuestion(questionId);

      var answers = GetAnswersForQuestion(questionId);

      var comment = new Comment();
      var commentList = new List<Comment>();

      List<Comment> instance = new List<Comment>();


      foreach (var answ in answers)
      {
        answ.Comments = new List<Comment>();
        var answerID = answ.AnswerId;
        comment = (from c in db.Comments
                   join a in db.Answers
                   on c.PostId equals a.AnswerId
                   where a.AnswerId == answerID
                   select new Comment
                   {
                     UserId = c.UserId,
                     PostId = c.PostId,
                     //Timestamp = c.Timestamp,
                     CommentId = c.CommentId,
                     CommentScore = c.CommentScore,
                     CommentText = c.CommentText
                   }).FirstOrDefault();

        if (comment != null)
        {

          answ.Comments.Add(comment);
        }
      }

      var fullPost = new FullPost()
      {
        Question = question,
        Answers = answers,
      };
      Console.WriteLine(fullPost);

      return fullPost;
    }

    public Comment GetComments(int postId)
    {
      using var db = new SOContext();

      var comments = (from a in db.Comments
                      join p in db.Posts
                      on a.PostId equals p.PostId
                      where a.PostId == postId
                      select new Comment
                      {
                        CommentText = a.CommentText,
                        CommentScore = a.CommentScore,
                        UserId = a.UserId,
                        PostId = a.PostId
                      }).FirstOrDefault();

      return comments;
    }

    public AnswerDbDto GetAnswer(int answerId)
    {
      using var db = new SOContext();

      var answers = (from a in db.Answers
                     join p in db.Posts
                     on a.AnswerId equals p.PostId
                     where a.AnswerId == answerId
                     select new AnswerDbDto
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
                     select new AnswerDbDto
                     {
                       AnswerId = p.PostId,
                       ParentId = a.PostId,
                       CreationDate = p.CreationDate,
                       Score = p.Score,
                       Body = p.Body
                     }).ToList();

      //var comments = (from a in db.Comments
      //                   join p in db.Answers
      //                   on a.PostId equals p.PostId
      //                   where a.PostId == questionId 
      //                   select new Comment
      //                   {
      //                       CommentText = a.CommentText,
      //                       CommentScore = a.CommentScore,
      //                       UserId = a.UserId,
      //                       PostId = a.PostId
      //                   }).FirstOrDefault();

      //var finalResult = new FinalQuestionAnswerCommentDto()
      //      {
      //          AnswerDb = answers,
      //          Comments = comments
      //      };

      //Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@ FInal result @@@@@@@@");
      //      Console.Write(answers);
      //      Console.Write(comments);
      //Console.WriteLine(finalResult);
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
      catch (Exception e)
      {
        return 0;
        throw e;
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
      catch (Exception e)
      {
        return false;
        throw e;
      }
    }

    public List<Annotation> GetAnnotationsByMarking(int markingId)
    {
      using var db = new SOContext();
      var annotations = db.Annotations.Where(a => a.MarkingId == markingId).ToList();

      return annotations;
    }

    public List<Annotation> GetAnnotationsByUser(int userId)
    {
      using var db = new SOContext();
      var annotations = (from a in db.Annotations
                         join m in db.Markings on a.MarkingId equals m.MarkingId
                         where m.UserId == userId
                         select new Annotation
                         {
                           AnnotationId = a.AnnotationId,
                           MarkingId = a.MarkingId,
                           Body = a.Body
                         }).ToList();

      return annotations;
    }

    public bool AddMarking(Marking marking)
    {
      using var db = new SOContext();

      try
      {
        db.Markings.Add(marking);
        db.SaveChanges();

        return true;
      }
      catch (Exception e)
      {
        return false;
        throw e;
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
      catch (Exception e)
      {
        return false;
        throw e;
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
      catch (Exception e)
      {
        Console.WriteLine(e);
        return false;
      }
    }
    public User GetUser(string username)
    {
      using var db = new SOContext();
      return db.Users.FirstOrDefault(x => x.Username == username);
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
      try
      {
        db.Users.Add(user);
        db.SaveChanges();
        Console.WriteLine("@@@@@@@@@@@@@ stored in db @@@@@@@@@@@@@@@@");
        return user;

      }
      catch (Exception e)
      {
        Console.WriteLine("@@@@@@@@@@@@@ failed stored in db @@@@@@@@@@@@@@@@");
        Console.Write(e);
        return user;
      }
    }

    //  public List<Post> GetAuthPosts(int userId)
    //     {
    //         var db = new SOContext();
    //         Console.WriteLine("@@@@@@@@@@@@@ USER ID @@@@@@@@@@@@@@@@");
    //         Console.WriteLine(@"User id = {0} ", userId);
    //         if (db.Users.FirstOrDefault(x => x.Id == userId) == null)
    //             throw new ArgumentException("user not found");
    //         return _posts;
    //     }

    public List<SearchResult> Search(string[] words)
    {
      using var db = new SOContext();

      try
      {
        var result = db.SearchResults
          .FromSqlRaw("select * from best_match_with_weight({0})", words)
          .ToList();

        return result;
      }
      catch (Exception e)
      {
        return null;
        throw e;
      }
    }
  }
}