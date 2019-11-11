using DatabaseService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/answers")]
    public class AnswersController: ControllerBase
    {
        IDataService _dataService;

        public AnswersController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public IList<Answer> GetAnswers([FromQuery]PagingAttributes pagingAttributes)
        {
           return _dataService.GetAnswers(pagingAttributes);
        }

        [HttpGet("{answerId}")]
        public ActionResult <Answer> GetAnswer(int answerId)
        {
            var answer = _dataService.GetAnswer(answerId);

            if (answer == null) return NotFound();

            return Ok(answer);
        }
    }
}
