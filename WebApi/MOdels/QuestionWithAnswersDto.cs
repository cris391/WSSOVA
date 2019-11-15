using System.Collections.Generic;
using DatabaseService;

namespace WebApi.Models
{
  public class QuestionWithAnswersDto
  {
    public Question Question { get; set; }
    public List<Answer> Answers { get; set; }
  }
}