using System;
using AutoMapper;
using DatabaseService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AnnotationsController : ControllerBase
  {
    IDataService _dataService;
    private IMapper _mapper;
    public AnnotationsController(IDataService dataService, IMapper mapper)
    {
      _dataService = dataService;
      _mapper = mapper;
    }

    [Authorize]
    [HttpPost]
    public ActionResult AddAnnotation([FromBody] Annotation annotation)
    {
      var result = _dataService.AddAnnotation(annotation);

      if (result == 0) return BadRequest();

      return Ok(result);
    }

    [Authorize]
    [HttpPut]
    public ActionResult UpdateAnnotation([FromBody] Annotation annotation)
    {
      var result = _dataService.UpdateAnnotation(annotation);

      if (result == false) return NotFound();

      return Ok(result);
    }

    [HttpGet("marking/{markingId}")]
    // public ActionResult GetAnnotations([FromBody] Annotation annotation)
    public ActionResult GetAnnotations(int markingId)
    {
      var result = _dataService.GetAnnotationsByMarking(markingId);

      if (result.Count == 0) return NoContent();

      return Ok(result);
    }

    [Authorize]
    [HttpGet]
    public ActionResult GetAnnotationsByUser()
    {
      var userId = Helpers.GetUserIdFromJWTToken(Request.Headers["Authorization"]);
      var result = _dataService.GetAnnotationsByUser(userId);

      if (result.Count == 0) return NoContent();

      return Ok(result);
    }

    [Authorize]
    [HttpDelete]
    public ActionResult DeleteAnnotation(Annotation annotation)
    {
      var result = _dataService.DeleteAnnotation(annotation);

      if (result == false) return BadRequest();

      return Ok(result);
    }
  }
}