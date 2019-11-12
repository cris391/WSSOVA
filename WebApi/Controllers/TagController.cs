using System.Collections.Generic;
using System.Linq;
using DatabaseService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.ApiModels;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/tags")]
    public class TagController : ControllerBase
    {
        IDataService _dataService;

        public TagController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public IList<Tag> GetTags()
        {
          return _dataService.GetTags();
        }

        [HttpPost]
        [Route("create")]
        public Tag CreateTag([FromBody] Tag tag)
        {
          return _dataService.CreateTag(tag);
        }
        
    }
}
