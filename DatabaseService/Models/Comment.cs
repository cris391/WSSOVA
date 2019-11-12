using System;
namespace DatabaseService
{
    public class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public int CommentScore { get; set; }
        public string CommentText { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
