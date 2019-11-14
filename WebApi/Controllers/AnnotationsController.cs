using System;
using AutoMapper;
using DatabaseService;
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

    [HttpPost]
    public ActionResult AddAnnotation([FromBody] Annotation annotation)
    {
      var result = _dataService.AddAnnotation(annotation);

      if (result == 0) return BadRequest();

      return Ok(result);
    }
    
    // [HttpGet("{annotationId}")]
    // public ActionResult GetAnnotation(int annotationId)
    // {
    //   var result = _dataService.GetAnnotation(annotationId);

    //   return Ok(result);
    // }
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

      if(result.Count == 0) return NoContent();

      return Ok(result);
    }

    [HttpGet("user/{userId}")]
    // public ActionResult GetAnnotations([FromBody] Annotation annotation)
    public ActionResult GetAnnotationsByUser(int userId)
    {
      var result = _dataService.GetAnnotationsByUser(userId);

      if(result.Count == 0) return NoContent();

      return Ok(result);
    }

    [HttpDelete]
    // public ActionResult GetAnnotations([FromBody] Annotation annotation)
    public ActionResult DeleteAnnotation(Annotation annotation)
    {
      var result = _dataService.DeleteAnnotation(annotation);

      return Ok(result);
    }

  // TODO fetch annotations by markingid and postid
    // [HttpGet("marking/post")]
    // // public ActionResult GetAnnotations([FromBody] Annotation annotation)
    // public ActionResult GetAnnotationsByMarkingAndPost()
    // {
    //   var result = _dataService.GetAnnotations(markingId);

    //   return Ok(result);
    // }
  }
}