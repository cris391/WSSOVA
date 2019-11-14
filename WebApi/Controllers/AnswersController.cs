using System.Collections.Generic;
using AutoMapper;
using DatabaseService;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AnswersController : ControllerBase
  {
    IDataService _dataService;
    private IMapper _mapper;
    public AnswersController(IDataService dataService, IMapper mapper)
    {
      _dataService = dataService;
      _mapper = mapper;
    }

    [HttpGet("{answerId}", Name = nameof(GetAnswer))]
    public ActionResult GetAnswer(int answerId)
    {
      var result = _dataService.GetAnswer(answerId);

      return Ok(CreateAnswerDto(result));
    }

    [HttpGet("question/{questionId}")]
    public ActionResult GetAnswersForQuestion(int questionId)
    {
      var result = _dataService.GetAnswersForQuestion(questionId);

      return Ok(CreateAnswerDtos(result));
      // return Ok(result);
    }

    ///////////////////
    //
    // Helpers
    //
    ///////////////////

    private AnswerDto CreateAnswerDto(AnswerDbDto answer)
    {
      var dto = _mapper.Map<AnswerDto>(answer);
      dto.Link = Url.Link(
              nameof(GetAnswer),
              new { answerId = answer.AnswerId });
      dto.LinkParent = Url.Link(
              nameof(GetAnswer),
              new { answerId = answer.ParentId });
      return dto;
    }

    private List<AnswerDto> CreateAnswerDtos(List<AnswerDbDto> answers)
    {
      List<AnswerDto> answerDtos = new List<AnswerDto>();
      foreach (var answer in answers)
      {
        // AnswerDto answerDto = new AnswerDto();
        var answerDto = _mapper.Map<AnswerDto>(answer);
        answerDto.Link = Url.Link(
              nameof(GetAnswer),
              new { answerId = answer.AnswerId });
        answerDto.LinkParent = Url.Link(
                nameof(GetAnswer),
                new { answerId = answer.ParentId });
        answerDtos.Add(answerDto);
      }
      return answerDtos;
    }
  }
}