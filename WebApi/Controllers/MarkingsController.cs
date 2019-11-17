using System;
using System.Collections.Generic;
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
    [HttpGet(Name = nameof(GetMarkings))]
    public ActionResult GetMarkings([FromQuery] PagingAttributes pagingAttributes)
    {
      var userId = Helpers.GetUserIdFromJWTToken(Request.Headers["Authorization"]);
      var result = _dataService.GetMarkings(userId, pagingAttributes);
      if (result.Count == 0) return NoContent();

      var markingDtos = CreateMarkingDtos(result);
      // return Ok(CreateMarkingDtos(result));
      return Ok(CreateResult(markingDtos, pagingAttributes));
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

      return Ok(CreateMarkingDto(marking));
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

    ///////////////////
    //
    // Helpers
    //
    //////////////////////

    private MarkingDto CreateMarkingDto(Marking marking)
    {
      var dto = _mapper.Map<MarkingDto>(marking);

      dto.Title = _dataService.GetQuestion(marking.PostId).Title;
      dto.LinkPost = Url.Link(
            nameof(QuestionsController.GetQuestion),
            new { questionId = marking.PostId });

      return dto;
    }

    private List<MarkingDto> CreateMarkingDtos(List<Marking> markings)
    {
      Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@");
      Console.WriteLine(markings);
      List<MarkingDto> markingDtos = new List<MarkingDto>();
      foreach (var marking in markings)
      {
        var markingDto = new MarkingDto();
        markingDto.Title = _dataService.GetQuestion(marking.PostId).Title;
        markingDto.LinkPost = Url.Link(
                nameof(QuestionsController.GetQuestion),
                new { questionId = marking.PostId });
        if (marking.Annotation != null)
        {
          var annotationDto = _mapper.Map<AnnotationDto>(marking.Annotation);
          markingDto.AnnotationDto.Link = Url.Link(
                nameof(AnnotationsController.UpdateAnnotation),
                new { annotationId = marking.Annotation.AnnotationId });
          markingDto.AnnotationDto.Body = annotationDto.Body;
        }
        markingDtos.Add(markingDto);
      }
      return markingDtos;
    }

    private object CreateResult(IEnumerable<MarkingDto> markings, PagingAttributes attr)
    {
      var totalItems = _dataService.NumberOfMarkings();
      var numberOfPages = Math.Ceiling((double)totalItems / attr.PageSize);

      var prev = attr.Page > 0
          ? CreatePagingLink(attr.Page - 1, attr.PageSize)
          : null;
      var next = attr.Page < numberOfPages - 1
          ? CreatePagingLink(attr.Page + 1, attr.PageSize)
          : null;

      return new
      {
        totalItems,
        numberOfPages,
        prev,
        next,
        items = markings
      };
    }

    private string CreatePagingLink(int page, int pageSize)
    {
      return Url.Link(nameof(GetMarkings), new { page, pageSize });
    }
  }
}