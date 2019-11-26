using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
  [Route("[controller]")]
  public class ReadyController : ControllerBase
  {
    private readonly IServiceProvider serviceProvider;
    private readonly IApiWarmer apiWarmer;
    public ReadyController(IApiWarmer apiWarmer)
    // public ReadyController(IServiceProvider serviceProvider, IApiWarmer apiWarmer)
    {
      // this.serviceProvider = serviceProvider;
      this.apiWarmer = apiWarmer;
    }

    private static bool isWarmedUp = false;

    private string GetFullUrl(string relativeUrl) =>
           $"{Request.Scheme}://{Request.Host}{relativeUrl}";

    public async Task Warmup()
    {
      using (var httpClient = new HttpClient())
      {
        // Warm up the /values endpoint.
        var valuesUrl = GetFullUrl(Url.Action("Get", "Values"));
        await httpClient.GetAsync(valuesUrl);

        // Here we could warm up some other endpoints too.
      }

      isWarmedUp = true;
    }
    private async Task Warm()
    {
      using (var client = new HttpClient())
      {
        // Warm up the /values endpoint.
        try
        {
          HttpResponseMessage response = await client.GetAsync("http://localhost:5001/ready");
          response.EnsureSuccessStatusCode();
          string responseBody = await response.Content.ReadAsStringAsync();

          Console.WriteLine(responseBody);
        }
        catch (HttpRequestException e)
        {
          Console.WriteLine("\nException Caught!");
          Console.WriteLine("Message :{0} ", e.Message); 
        }
      }
    }


    [HttpGet, HttpHead]
    public async Task<IActionResult> Get()
    {
      if (!isWarmedUp)
      {
        await apiWarmer.WarmUp<Startup>(serviceProvider, Url, Request, "Ready");

        isWarmedUp = true;
      }

      return Ok("API is ready!");
    }
  }
}