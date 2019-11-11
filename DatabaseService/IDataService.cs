using System;
using System.Collections.Generic;

namespace DatabaseService
{
  public interface IDataService
  {
    List<Question> GetQuestions();
    Question GetQuestion(int id);

    QuestionDto GetFullQuestion(int id);

    List<Answer> GetAnswers();
    Answer GetAnswer(int id);

    List<Post> GetPosts();
    Post GetPost(int id);
        // Auth
    List<Post> GetAuthPosts(int userId);


    List<Annotation> GetAnnotations(int userId);
    Annotation GetAnnotation(int userId, int questionId);
    Annotation CreateAnnotation(int userId, int questionId, string body);
    bool DeleteAnnotation(int userId, int questionId);
    bool UpdateAnnotation(int userId, int questionId, string body);


    User GetUser(string username);
    User CreateUser(string username, string password, string salt);


    }
}