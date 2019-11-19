using System;

namespace WebApi.Models
{
  public class OwnerDto
  {
    public string Link { get; set; }
    public DateTime UserCreationDate { get; set; }
    public string UserDisplayName { get; set; }
    public string UserLocation { get; set; }
    public int UserAge { get; set; }
  }
}

