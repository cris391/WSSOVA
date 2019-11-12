using System;
using System.ComponentModel.DataAnnotations;

namespace DatabaseService
{
    public class Annotation
    {
      
        public int UserId { get; set; }
        [Key]
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string? Body { get; set; }
    }
}
