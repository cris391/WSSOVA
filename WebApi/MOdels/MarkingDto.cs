using DatabaseService;

namespace WebApi.Models
{
  public class MarkingDto
  {
    public string Title { get; set; }
    public string LinkPost { get; set; }

    public AnnotationDto? AnnotationDto { get; set; }
  }
}
