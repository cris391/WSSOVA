using System;
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
      if (result.Count == 0) return NoContent();
      return Ok(result);
    }

    [HttpPost]
    public ActionResult AddMarking([FromBody] MarkingForCreation markingDto)
    {
      var marking = _mapper.Map<Marking>(markingDto);
      var result = _dataService.AddMarking(marking);

      if (result == false) return BadRequest();

      return Ok(result);
    }

    [HttpDelete]
    public ActionResult DeleteMarking(Marking marking)
    {
      var result = _dataService.DeleteMarking(marking);

      if (result == false)
      {
        return NotFound();
      }

      return Ok(result);
    }
  }
}