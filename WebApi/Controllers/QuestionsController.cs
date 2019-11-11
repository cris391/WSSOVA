using DatabaseService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
//using WebApi.Models;

namespace WebApi.Controllers
{
  [ApiController]
  [Route("api/questions")]
  public class QuestionsController : ControllerBase
  {
    IDataService _dataService;
    public QuestionsController(IDataService dataService)
    {
      _dataService = dataService;
    }

      [HttpGet]
      public IList<Question> GetQuestions()
      {
        return _dataService.GetQuestions();
      }

    /*
      [HttpGet("{questionId}")]
      public ActionResult<Question> GetQuestion(int questionId)
      {
     
        var question = _dataService.GetQuestion(questionId);
        if (question == null) return NotFound();

        return Ok(question);
       }
     */


        [HttpGet("{questionId}")]
        public ActionResult<Question> GetFullQuestion(int questionId)
        {
            var question = _dataService.GetFullQuestion(questionId);

            if (question == null) return NotFound();

            return Ok(question);
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
    }
}
