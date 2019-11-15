using System;
using DatabaseService;

namespace WebApi.MOdels
{
    public class QuestionCommentDto
    {
        public Question Question { get; set; }
        public Comment Comment { get; set; }
    }
}
