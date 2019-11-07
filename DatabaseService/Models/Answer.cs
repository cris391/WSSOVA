using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseService
{
  public class Answer
  {

    public int AnswerId { get; set; }
    // public DateTime CreationDate { get; set; }
    // public int Score { get; set; }
    // public string Body { get; set; }


    public int PostId { get; set; }
    public Post Post { get; set; }

    // public int QuestionForeignKey { get; set; }
    // public Question Question { get; set; }
  }
}