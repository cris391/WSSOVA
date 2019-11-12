using AutoMapper;
using DatabaseService;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class MarkingsController : ControllerBase
  {
    IDataService _dataService;
    private IMapper _mapper;
    public MarkingsController(IDataService dataService, IMapper mapper)
    {
      _dataService = dataService;
      _mapper = mapper;
    }

    [HttpGet("{userId}")]
    public ActionResult GetMarkings(int userId)
    {
      var result = _dataService.GetMarkings(userId);

      return Ok(result);
    }

    [HttpPost]
    public ActionResult AddMarking([FromBody] MarkingForCreation markingDto)
    {
      var marking = _mapper.Map<Marking>(markingDto);
      var result = _dataService.CreateMarking(marking);

      return Ok(result);
    }

    [HttpDelete]
    public ActionResult DeleteMarkings(Marking marking)
    {
      var result = _dataService.DeleteMarkings(marking);

      return Ok(result);
    }
  }
}