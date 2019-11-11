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
    [Route("api/posts")]
    public class PostsController: ControllerBase
    {
        IDataService _dataService;

        public PostsController(IDataService dataService)
        {
            _dataService = dataService;
        }

        
        [HttpGet]
        public List<Post> GetPosts()
        {
            return _dataService.GetPosts();
        }

     
        [HttpGet]
        [Route("{postId}")]
        public ActionResult<Post> GetPost(int postId)
        {
            var post = _dataService.GetPost(postId);

            if (post == null) return NotFound();

            return Ok(post);
        }


    }

   
}
