using System;
using System.Net.Http;
using System.Threading.Tasks;

public class Test
{

  public async Task Warmup()
  {
    using (var httpClient = new HttpClient())
    {
      // Warm up the /values endpoint.
      // var valuesUrl = GetFullUrl(Url.Action("Get", "Values"));
      await httpClient.GetAsync("http://localhost:5001/ready");
      Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@");
      Console.WriteLine();

      // Here we could warm up some other endpoints too.
    }

  }
}