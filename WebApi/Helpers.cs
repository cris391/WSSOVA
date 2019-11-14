using System.Collections.Generic;
using WebApi.Models;

namespace DatabaseService
{
  class Helpers
  {
    // public static List<QuestionDto> CreateQuestionDtos(List<Question> questions)
    // {
    //   List<QuestionDto> questionDtos = new List<QuestionDto>();
    //   foreach (var question in questions)
    //   {
    //     QuestionDto questionDto = new QuestionDto()
    //     {
    //       Link = Url.Link(
    //           nameof("GetQuestions"),
    //           new { questionId = question.QuestionId }),
    //       Title = question.Title,
    //       QuestionId = question.QuestionId,
    //       CreationDate = question.Post.CreationDate,
    //       Score = question.Post.Score,
    //       Body = question.Post.Body,
    //       PostId = question.Post.PostId
    //     };
    //     questionDtos.Add(questionDto);
    //   }
    //   return questionDtos;
    // }
  }
}