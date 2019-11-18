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
    #nullable enable
    public string? UserLocation { get; set; }
    #nullable enable
    public int? UserAge { get; set; }
  }
}