using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseService
{
  public class Question
  {
    public int QuestionId { get; set; }
    public DateTime? ClosedDate { get; set; }
    public string Title { get; set; }
    public int? AcceptedAnswerId { get; set; }
    public Post Post { get; set; }

    // public int AnswerId { get; set; }
    // public Answer Answer { get; set; }
    // public List<Answer> Answers { get; set; }
  }
}