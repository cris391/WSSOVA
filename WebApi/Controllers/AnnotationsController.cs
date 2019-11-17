using System.Collections.Generic;
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
      annotation.AnnotationId = result;

      return Ok(CreateAnnotationDto(annotation));
    }

    [Authorize]
    [HttpPut("{annotationId}", Name = nameof(UpdateAnnotation))]
    public ActionResult UpdateAnnotation([FromBody] Annotation annotation, int annotationId)
    {
      annotation.AnnotationId = annotationId;
      var result = _dataService.UpdateAnnotation(annotation);

      if (result == false) return NotFound();

      return Ok(CreateAnnotationDto(annotation));
    }

    [HttpGet("marking/{markingId}")]
    public ActionResult GetAnnotations(int markingId)
    {
      var result = _dataService.GetAnnotationsByMarking(markingId);

      if (result.Count == 0) return NoContent();

      return Ok(result);
    }

    [Authorize]
    [HttpGet]
    public ActionResult<AnnotationDto> GetAnnotationsByUser()
    {
      var userId = Helpers.GetUserIdFromJWTToken(Request.Headers["Authorization"]);
      var result = _dataService.GetAnnotationsByUser(userId);

      if (result.Count == 0) return NoContent();

      return Ok(CreateAnnotationDtos(result));
    }

    //todo delete annotation by specifying annotationId in the url(vs body)
    [Authorize]
    [HttpDelete]
    public ActionResult DeleteAnnotation(Annotation annotation)
    {
      var result = _dataService.DeleteAnnotation(annotation);

      if (result == false) return BadRequest();

      return Ok(result);
    }


    ///////////////////
    //
    // Helpers
    //
    //////////////////////

    private AnnotationDto CreateAnnotationDto(Annotation annotation)
    {
      var dto = _mapper.Map<AnnotationDto>(annotation);

      dto.Link = Url.Link(
            nameof(AnnotationsController.UpdateAnnotation),
            new { annotationId = annotation.AnnotationId });

      return dto;
    }

    private List<AnnotationDto> CreateAnnotationDtos(List<Annotation> annotations)
    {
      List<AnnotationDto> dtos = new List<AnnotationDto>();
      foreach (var annotation in annotations)
      {
        dtos.Add(new AnnotationDto
        {
          Link = Url.Link(
            nameof(AnnotationsController.UpdateAnnotation),
            new { annotationId = annotation.AnnotationId }),
          Body = annotation.Body
        });
      }

      return dtos;
    }
  }
}