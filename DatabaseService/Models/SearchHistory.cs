using System;

namespace DatabaseService
{
  public class SearchHistory
  {
    public DateTime SearchDate { get; set; }
    public string QueryText { get; set; }
    public int UserId { get; set; }
  }
}
