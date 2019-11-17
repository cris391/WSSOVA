using System;
using AutoMapper;
using DatabaseService;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    [HttpGet]
    public ActionResult GetMarkings()
    {
      var userId = Helpers.GetUserIdFromJWTToken(Request.Headers["Authorization"]);
      var result = _dataService.GetMarkings(userId);
      if (result.Count == 0) return NoContent();
      return Ok(result);
    }

    [Authorize]
    [HttpPost]
    public ActionResult AddMarking([FromBody] MarkingForCreation markingDto)
    {
      var userId = Helpers.GetUserIdFromJWTToken(Request.Headers["Authorization"]);
      markingDto.UserId = userId;
      var marking = _mapper.Map<Marking>(markingDto);
      var result = _dataService.AddMarking(marking);

      if (result == false) return Conflict();

      return Ok(result);
    }

    [Authorize]
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