using System;

namespace DatabaseService
{
    public class Post
    {
        public int Id { get; set; }
        public DateTime? CreationDate { get; set; }
        public int Score { get; set; }
        public string Body { get; set; }

        //public Question Question { get; set; }
    }
}
