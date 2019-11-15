using System;
using System.Collections.Generic;

namespace DatabaseService
{
    public class FullPost
    {
        public Question Question { get; set; }
        public List<AnswerDbDto> Answers { get; set; }      
    }
}
