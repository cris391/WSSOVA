namespace DatabaseService
{
    public class Annotation
    {
        public int AnnotationId { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public string Body { get; set; }
    }
}