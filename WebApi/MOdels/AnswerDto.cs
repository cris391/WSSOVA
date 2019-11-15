using System;

namespace WebApi.Models
{
  public class AnswerDto
  {
    public string Link { get; set; }
    public string LinkParent { get; set; }
    public DateTime CreationDate { get; set; }
    public int Score { get; set; }
    public string Body { get; set; }
  }
}

