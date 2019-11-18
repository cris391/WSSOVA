using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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

    public int NumberOfMarkings()
    {
      using var db = new SOContext();
      return db.Markings.Count();
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

    // due to efcore 3.0 still not supporting multiple dataset return we will query the db multiple times for due to efcore's simplicity
    public FullPost GetFullPost(int questionId)
    {
      try
      {
        using var db = new SOContext();
        var questionDbDto = (from q in db.Questions
                             join p in db.Posts
                             on q.QuestionId equals p.PostId
                             join o in db.Owners
                             on p.OwnerId equals o.UserId
                             where q.QuestionId == questionId
                             select new QuestionDbDto
                             {
                               QuestionId = q.QuestionId,
                               Title = q.Title,
                               CreationDate = p.CreationDate,
                               ClosedDate = q.ClosedDate,
                               Score = p.Score,
                               Body = p.Body,
                               Owner = o
                             }).FirstOrDefault();
        // Console.WriteLine(JsonConvert.SerializeObject(questionDbDto));
        var questionDbDtoComments = db.Comments
                                      .Where(c => c.PostId == questionId)
                                      .ToList();

        // assign comments to question
        questionDbDto.Comments = questionDbDtoComments;

        var answerDbDtos = (from a in db.Answers
                            join p in db.Posts
                            on a.AnswerId equals p.PostId
                            join o in db.Owners
                            on p.OwnerId equals o.UserId
                            where a.PostId == questionId
                            select new AnswerDbDto
                            {
                              AnswerId = a.AnswerId,
                              CreationDate = p.CreationDate,
                              Score = p.Score,
                              Body = p.Body,
                              Owner = o
                            }).ToList();

        var answerIds = new List<int>();
        foreach (var item in answerDbDtos)
        {
          answerIds.Add(item.AnswerId);
        }
        var answerDbDtoComments = db.Comments
                                    .Where(c => answerIds.Contains(c.PostId))
                                    .ToList();
        foreach (var answer in answerDbDtos)
        {
          answer.Comments = answerDbDtoComments.Where(c => c.PostId == answer.AnswerId).ToList();
        }

        var marking = db.Markings
            .Where(m => m.PostId == questionId)
            .FirstOrDefault();
        Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@");
        Console.WriteLine(marking);

        var tags = GetQuestionTags(questionId);

        // map questions and answers + comments to post
        FullPost post = new FullPost();
        post.Question = questionDbDto;
        post.Question.Marking = marking;
        post.Answers = answerDbDtos;
        post.Tags = tags;

        return post;
      }
      catch (Exception e)
      {
        // return null;
        throw e;
      }
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
      Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@");
      Console.WriteLine(annotations.Count);

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
    public List<Marking> GetMarkings(int userid, PagingAttributes pagingAttributes)
    {
      using var db = new SOContext();

      var markings = db.Markings
        .Where(m => m.UserId == userid)
        .Include(m => m.Annotation)
        .Skip(pagingAttributes.Page * pagingAttributes.PageSize)
        .Take(pagingAttributes.PageSize)
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
      var user = db.Users.FirstOrDefault(x => x.Username == username);
      return user;
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
        return user;

      }
      catch (Exception e)
      {
        return user;
        throw e;
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

    public List<SearchResult> Search(string[] words, int userId)
    {
      using var db = new SOContext();

      // small hack due to FromSqlRaw parser issue
      string str = "";
      foreach (var item in words)
      {
        str += $",'{item}'";
      }
      try
      {
        var result = db.SearchResults
          .FromSqlRaw($"select * from best_match_with_weight({userId} {str})")
          .ToList();

        return result;
      }
      catch (Exception e)
      {
        // return null;
        throw e;
      }
    }

    public Tag GetQuestionTags(int questionId)
    {
      using var db = new SOContext();

      try
      {
        var tags = db.Tags
                      .Where(t => t.QuestionId == questionId)
                      .FirstOrDefault();
        return tags;
      }
      catch (Exception)
      {
        return null;
      }
    }

    public List<SearchHistory> GetSearchHistory(int userId)
    {
      using var db = new SOContext();

      try
      {
        var result = db.SearchHistories
                        .Where(h => h.UserId == userId)
                        .ToList();
        return result;
      }
      catch (Exception e)
      {
        return null;
        throw e;
      }
    }
    public Owner GetOwner(int ownerId)
    {
      using var db = new SOContext();

      try
      {
        var result = db.Owners.Find(ownerId);

        return result;
      }
      catch (Exception e)
      {
        // return null;
        throw e;
      }
    }

  }
}