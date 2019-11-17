using System;
using System.Collections.Generic;

namespace DatabaseService
{
  public class QuestionDbDto
  {
    public int QuestionId { get; set; }
    public string Title { get; set; }
    public DateTime? CreationDate { get; set; }
    public DateTime? ClosedDate { get; set; }
    public int Score { get; set; }
    public string Body { get; set; }

    public Owner Owner { get; set; }

    public List<Comment> Comments { get; set; }
  }
}
