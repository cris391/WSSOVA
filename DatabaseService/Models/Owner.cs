using System;
using System.ComponentModel.DataAnnotations;

namespace DatabaseService
{
  public class Owner
  {
    [Key]
    public int UserId { get; set; }
    public DateTime UserCreationDate { get; set; }
    public string UserDisplayName { get; set; }
    public string UserLocation { get; set; }
    public int UserAge { get; set; }
  }
}