using System.ComponentModel.DataAnnotations;

namespace DatabaseService
{
  public class MarkingForCreation
  {
    [Required]
    public int UserId { get; set; }
    [Required]
    public int PostId { get; set; }
  }
}