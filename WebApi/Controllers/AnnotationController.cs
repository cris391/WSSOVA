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
    public class AnnotationController
    {

        /*
        IDataService _dataService;

        public AnnotationController(IDataService dataService)
        {
            _dataService = dataService;
        }
        */

        /*
        [HttpGet]
        public IList<Annotation> GetAnnotation()
        {
            return _dataService.GetAnnotations();
        }
        */

        //[HttpPost]
        //public ActionResult CreateAnnotation([FromBody] Annotation annotation)
        //{
        //    var anno = _dataService.CreateAnnotation(annotation.Id, annotation.Body);

        //    return Created("annotation", anno);
        //}
    }
}
