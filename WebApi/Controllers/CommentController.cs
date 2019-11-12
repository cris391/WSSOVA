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
    [Route("api/comments")]
    public class CommentController: ControllerBase
    {
        IDataService _dataService;

        public CommentController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public IList <Comment> GetComments([FromQuery]PagingAttributes paginAttributes)
        {
           return _dataService.GetComments(paginAttributes);
        }

        [HttpPost]
        [Route("create")]
        public Comment CreateComment([FromBody] Comment comment)
        {
            var postId = 1;
            var userId = 1;
          return _dataService.CreateComment(postId, userId, comment);   
        }

    }
}
