using System;

namespace DatabaseService
{
  public class SearchHistoryDto
  {
    public DateTime SearchDate { get; set; }
    public string QueryText { get; set; }
  }
}
