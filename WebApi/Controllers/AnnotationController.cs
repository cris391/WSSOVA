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
    [Route("api/annotation")]
    public class AnnotationController: ControllerBase
    {
        IDataService _dataService;

        public AnnotationController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet("{userId}")]
        public IList<Annotation> GetAnnotations(int userId)
        {
            return _dataService.GetAnnotations(userId);
        }

        [HttpGet("{userId}/{questionId}")]
        public Annotation GetAnnotation(int userId, int questionId)
        {
            return _dataService.GetAnnotation(userId, questionId);
        }


        [HttpPost]
        [Route("api/annotation/{userId}/{questionId}")]
        public ActionResult CreateAnnotation([FromBody] Annotation annotation)
        {
            _dataService.CreateAnnotation(annotation);

            return Ok(annotation);
            //return Created("annotation", anno);
        }


        [HttpDelete("delete/{userId}/{questionId}")]
        public ActionResult DeleteAnnotation(int userId, int questionId)
        {
            if (_dataService.DeleteAnnotation(userId, questionId))
            {
                return NoContent();
            }

            return NotFound();
        }


        [HttpPut("update/{userId}/{questionId}")]
        public ActionResult UpdateAnnotation([FromBody] Annotation annotation, int userId, int questionId, string body)
        {
            var anno = _dataService.UpdateAnnotation(userId, questionId, body);

            if (anno == false)
            {
                return NotFound();
            }
            return Ok(anno);
        }
    }
}
