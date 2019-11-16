using DatabaseService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class TagsController : ControllerBase
  {
    IDataService _dataService;
    public TagsController(IDataService dataService)
    {
      _dataService = dataService;
    }

    [HttpGet("{questionId}")]
    public ActionResult GetQuestionTags(int questionid)
    {
      var tags = _dataService.GetQuestionTags(questionid);
      if (tags == null) return NoContent();

      return Ok(tags);
    }

  }

}
