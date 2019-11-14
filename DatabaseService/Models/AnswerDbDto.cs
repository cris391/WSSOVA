using System;
namespace DatabaseService
{
  public class AnswerDbDto
  {
    public int AnswerId { get; set; }
    public int ParentId { get; set; }
    public DateTime CreationDate { get; set; }
    public int Score { get; set; }
    public string Body { get; set; }
  }
}