using System;
using System.Collections.Generic;

namespace DatabaseService
{
  public interface IDataService
  {
    List<Question> GetQuestions(PagingAttributes pagingAttributes);
    Question GetQuestion(int id);
    int NumberOfQuestions();
    List<Post> GetPosts();
    object GetQuestionWithAnswers(int questionId);
    AnswerDbDto GetAnswer(int answerId);
    List<AnswerDbDto> GetAnswersForQuestion(int questionId);
    FullPost GetFullPost(int questionId);
    Comment GetComments(int postId);
    int AddAnnotation(Annotation annotation);
    Annotation GetAnnotation(int annotationId);
    bool UpdateAnnotation(Annotation annotation);
    List<Annotation> GetAnnotations(int markingId);
    bool CreateMarking(Marking marking);
    List<Marking> GetMarkings(int userid);
    bool DeleteMarking(Marking marking);
    bool DeleteAnnotation(Annotation annotation);
    User GetUser(string username);
    User CreateUser(string username, string password, string salt);
  }
}