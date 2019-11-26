using System;

namespace WebApi.Models
{
  public class QuestionDto
  {
    public string Link { get; set; }
    public string LinkPost { get; set; }
    public string Title { get; set; }
    public int AcceptedAnswerId { get; set; }

    public int QuestionId { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ClosedDate { get; set; }

    public int Score { get; set; }
    public string Body { get; set; }
  }
}