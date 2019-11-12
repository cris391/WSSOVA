using AutoMapper;
using DatabaseService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public ActionResult<Question> GetQuestion(int questionId)
    {
      var question = _dataService.GetQuestion(questionId);

      if (question == null) return NotFound();

      Console.WriteLine("@@@@");
      // Console.WriteLine(question.Post.Body);
      Console.WriteLine(question.ClosedDate);

      return Ok(CreateQuestionDto(question));
    }

    // [HttpGet("answers/{questionId}")]
    // public ActionResult GetQuestionWithAnswers(int questionId)
    // {
    //   var result = _dataService.GetQuestionWithAnswers(questionId);
    //   Console.WriteLine(result);

    //   return Ok(result);
    // }
    //   [HttpPost]
    //   public ActionResult CreateCategory([FromBody] Category category)
    //   {
    //     var cat = _dataService.CreateCategory(category.Name, category.Description);

    //     return Created("post", cat);
    //   }

    //   [HttpPut("{categoryId}")]
    //   public ActionResult PutCategory([FromBody] Category category, int categoryId)
    //   {
    //     var cat = _dataService.PutCategory(categoryId, category.Name, category.Description);
    //     if (cat == false)
    //     {
    //       return NotFound();
    //     }

    //     return Ok(cat);
    //   }

    //   [HttpDelete("{categoryId}")]
    //   public ActionResult<Category> DeleteCategory(int categoryId)
    //   {
    //     // var category = _dataService.DeleteCategory(categoryId);

    //     if (_dataService.DeleteCategory(categoryId) == false)
    //     {
    //       return NotFound();
    //     }

    //     return Ok();
    //   }

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
          Title = question.Title,
          QuestionId = question.QuestionId,
          CreationDate = question.Post.CreationDate,
          Score = question.Post.Score,
          Body = question.Post.Body,
          PostId = question.Post.PostId
        };
        questionDtos.Add(questionDto);
      }
      return questionDtos;
    }
  }
}
