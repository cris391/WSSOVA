using System;
using System.Collections.Generic;

namespace DatabaseService
{
  public interface IDataService
  {
    List<Question> GetQuestions(PagingAttributes pagingAttributes);
    Question GetQuestion(int id);

    QuestionDto GetFullQuestion(int id);

    List<Answer> GetAnswers(PagingAttributes pagingAttributes);
    Answer GetAnswer(int id);

    List<Post> GetPosts(PagingAttributes pagingAttributes);
    Post GetPost(int id);
    List<Post> GetAuthPosts(int userId);


    List<Annotation> GetAnnotations(int userId);
    Annotation GetAnnotation(int userId, int questionId);
    void CreateAnnotation(Annotation annotation);
    bool DeleteAnnotation(int userId, int questionId);
    bool UpdateAnnotation(int userId, int questionId, string body);


    User GetUser(string username);
    User CreateUser(string name, string username, string password, string salt);


    }
}