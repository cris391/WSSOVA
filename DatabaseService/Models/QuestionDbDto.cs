using System;
namespace DatabaseService
{
    public class QuestionDbDto
    {
        public int QuestionId { get; set; }
        public DateTime? CreationDate { get; set; }
        public int Score { get; set; }
        public string Body { get; set; }
        public int PostId { get; set; }
    }
}
