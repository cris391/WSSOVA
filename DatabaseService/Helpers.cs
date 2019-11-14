using System;
using System.Collections.Generic;

namespace DatabaseService
{
    class Helpers
    {
        /* Mapping new Dto from Question merged with Post */

        public static  QuestionDto CreateQuestionDtos(Question question)
        {
              QuestionDto questionDto = new QuestionDto()
              {
                QuestionId = question.QuestionId,
                CreationDate = question.Post.CreationDate,
                Score = question.Post.Score,
                Body = question.Post.Body,
                PostId = question.Postid
              };
            return questionDto;
       }
    }
}