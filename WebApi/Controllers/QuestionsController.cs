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

    [HttpGet]
    public IList<Question> GetQuestions()
    {
      return _dataService.GetQuestions();
    }

    [HttpGet("{questionId}", Name = nameof(GetQuestion))]
    public ActionResult<Question> GetQuestion(int questionId)
    {
      var question = _dataService.GetQuestion(questionId);

      if (question == null) return NotFound();

      return Ok(CreateLink(question));
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

    private QuestionDto CreateLink(Question question)
    {
      var dto = _mapper.Map<QuestionDto>(question);
      dto.Link = Url.Link(
              nameof(GetQuestion),
              new { questionId = question.Id });
      return dto;
    }
  }
}
