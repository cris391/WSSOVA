using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseService
{
  public class Answer
  {

    public int AnswerId { get; set; }
    public int ParentId { get; set; }
    public int PostId { get; set; }


    // public int QuestionForeignKey { get; set; }
    // public Question Question { get; set; }
  }
}