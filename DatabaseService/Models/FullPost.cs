﻿using System.Collections.Generic;

namespace DatabaseService
{
    public class FullPost
    {
        public QuestionDbDto Question { get; set; }
        public List<AnswerDbDto> Answers { get; set; }      
        public Tag Tags { get; set; }
    }
}
