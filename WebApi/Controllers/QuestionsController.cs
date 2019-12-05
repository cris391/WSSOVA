using AutoMapper;
using DatabaseService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Controllers
{
  [ApiController]
  [Route("api/questions")]
  public class QuestionsController : ControllerBase
  {
    readonly IDataService _dataService;
    private readonly IMapper _mapper;
    public QuestionsController(IDataService dataService, IMapper mapper)
    {
      _dataService = dataService;
      _mapper = mapper;
    }

    [HttpGet(Name = nameof(GetQuestions))]
    public ActionResult GetQuestions([FromQuery] PagingAttributes pagingAttributes)
    {
      var questions = _dataService.GetQuestions(pagingAttributes);
      // var result = CreateResult(questions, pagingAttributes);
      var questionDtos = CreateQuestionDtos(questions);

      // return Ok(questionDtos);
      return Ok(CreateResult(questionDtos, pagingAttributes));
    }

    [HttpGet("{questionId}", Name = nameof(GetQuestion))]
    public ActionResult<QuestionDbDto> GetQuestion(int questionId)
    {
      var question = _dataService.GetQuestion(questionId);

      var dto = _mapper.Map<QuestionDto>(question);

      if (question == null) return NotFound();

      return Ok(CreateQuestionDtoFromDb(dto));
    }

    [HttpGet("full/{questionId}", Name = nameof(GetFullPost))]
    public ActionResult GetFullPost(int questionId)
    {
      var result = _dataService.GetFullPost(questionId);

      if (result == null) return NoContent();

      return Ok(result);
    }

    ///////////////////
    //
    // Helpers
    //
    //////////////////////

    private QuestionDto CreateQuestionDto(Question question)
    {
      var dto = _mapper.Map<QuestionDto>(question);
      dto.Link = Url.Link(
              nameof(GetQuestion),
              new { questionId = question.QuestionId });
      dto.Body = question.Post.Body;
      return dto;
    }

    private QuestionDto CreateQuestionDtoFromDb(QuestionDto question)
    {
      var dto = _mapper.Map<QuestionDto>(question);
      dto.Link = Url.Link(
              nameof(GetQuestion),
              new { questionId = question.QuestionId });
      dto.LinkPost = Url.Link(
           nameof(GetFullPost),
           new { questionId = question.QuestionId });
      dto.Body = question.Body;
      return dto;
    }

    private object CreateResult(IEnumerable<QuestionDto> questions, PagingAttributes attr)
    {
      var totalItems = _dataService.NumberOfQuestions();
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
        items = questions
      };
    }

    private string CreatePagingLink(int page, int pageSize)
    {
      return Url.Link(nameof(GetQuestions), new { page, pageSize });
    }

    private List<QuestionDto> CreateQuestionDtos(List<Question> questions)
    {
      List<QuestionDto> questionDtos = new List<QuestionDto>();
      foreach (var question in questions)
      {
        QuestionDto questionDto = new QuestionDto()
        {
          Link = Url.Link(
              nameof(GetQuestion),
              new { questionId = question.QuestionId }),
          LinkPost = Url.Link(
           nameof(GetFullPost),
           new { questionId = question.QuestionId }),
          Title = question.Title,
          QuestionId = question.QuestionId,
          CreationDate = question.Post.CreationDate,
          Score = question.Post.Score,
          Body = question.Post.Body,
        };
        questionDtos.Add(questionDto);
      }
      return questionDtos;
    }
  }
}
