using DatabaseService;

namespace WebApi.Models
{
  public class QuestionDto
  {
    public string Link { get; set; }
    public string Title { get; set; }
    public int AcceptedAnswerId { get; set; }
    public Post Post { get; set; }
  }
}