
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
  [ApiController]
  [Route("api/words")]
  public class WordsController : ControllerBase
  {
    [HttpGet]
    public ActionResult GetWords()
    {
      var words = new object[]
      {
                new {text = "Lorem", weight = 13},
                new {text = "Ipsum", weight = 10.5},
                new {text = "Dolor", weight = 9.4},
                new {text = "Sit", weight = 8},
                new {text = "Amet", weight = 6.2},
                new {text = "Consectetur", weight = 5},
                new {text = "Adipiscing", weight = 5},
                new {text = "Testing", weight = 15}
      };
      return Ok(words);
    }
  }
}