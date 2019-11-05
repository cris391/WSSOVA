using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
  public class QuestionForCreation
  {
    [Required]
    [MaxLength(80)]
    public string Title { get; set; }
  }
}