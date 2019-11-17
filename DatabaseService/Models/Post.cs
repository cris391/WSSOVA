using System;
namespace DatabaseService
{
  public class Post
  {

    public int PostId { get; set; }
    public DateTime CreationDate { get; set; }
    public int Score { get; set; }
    public string Body { get; set; }

    public int OwnerId { get; set; }
    public Owner Owner { get; set; }
  }
}