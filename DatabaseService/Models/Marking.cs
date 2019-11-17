namespace DatabaseService
{
  public class Marking
  {
    public int UserId { get; set; }
    public int PostId { get; set; }

    public int MarkingId { get; set; }
    public Annotation Annotation { get; set; }
  }
}