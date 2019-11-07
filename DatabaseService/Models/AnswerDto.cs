using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseService
{
  public class AnswerDto
  {

    public int AnswerId { get; set; }
    public DateTime CreationDate { get; set; }
    public int Score { get; set; }
    public string Body { get; set; }
    public int PostId { get; set; }
  }
}