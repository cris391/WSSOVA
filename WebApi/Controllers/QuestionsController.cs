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
    IDataService _dataService;
    private IMapper _mapper;
    public QuestionsController(IDataService dataService, IMapper mapper)
    {
      _dataService = dataService;
      _mapper = mapper;
    }

    [HttpGet(Name = nameof(GetQuestions))]
    public ActionResult GetQuestions([FromQuery] PagingAttributes pagingAttributes)
    {
      var questions = _dataService.GetQuestions(pagingAttributes);
      var result = CreateResult(questions, pagingAttributes);

      return Ok(result);
    }
    [HttpGet("answers")]
    public ActionResult GetQuestionWithAnswers()
    {
      var result = _dataService.GetQuestionWithAnswers(10662902);
      Console.WriteLine(result);

      return Ok(result);
    }

    [HttpGet("{questionId}", Name = nameof(GetQuestion))]
    public ActionResult<Question> GetQuestion(int questionId)
    {
      var question = _dataService.GetQuestion(questionId);

      if (question == null) return NotFound();

      return Ok(CreateQuestionDto(question));
    }

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
      return dto;
    }

    private object CreateResult(IEnumerable<Question> questions, PagingAttributes attr)
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
        items = questions.Select(CreateQuestionDto)
      };
    }

    private string CreatePagingLink(int page, int pageSize)
    {
      return Url.Link(nameof(GetQuestions), new { page, pageSize });
    }
  }
}
