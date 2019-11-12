using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseService
{
    public class Answer
    {
            public int Id { get; set; }
            public int parentId { get; set; }
            public int PostId { get; set; }
    }
}
