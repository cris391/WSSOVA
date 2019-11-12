using System;
namespace DatabaseService
{
    public class QuestionDto
    {
        public int QuestionId { get; set; }
        public DateTime? CreationDate { get; set; }
        public int Score { get; set; }
        public string Body { get; set; }
        public int PostId { get; set; }
    }
}
