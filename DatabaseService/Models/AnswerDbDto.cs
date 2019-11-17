using System;
using System.Collections.Generic;

namespace DatabaseService
{
  public class AnswerDbDto
  {
    public int AnswerId { get; set; }
    public int ParentId { get; set; }
    public DateTime CreationDate { get; set; }
    public int Score { get; set; }
    public string Body { get; set; }

    public Owner Owner { get; set; }
    public List <Comment> Comments { get; set; }
    }
}