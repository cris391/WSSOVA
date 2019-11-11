﻿using System.Linq;
using DatabaseService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebServiceToken.Models;

namespace WebServiceToken.Controllers
{
    [ApiController]
    [Route("api/auth/posts")]
    public class PostsController: Controller
    {
        private readonly IDataService _dataService;

        public PostsController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAuthPosts()
        {
            int.TryParse(HttpContext.User.Identity.Name, out var id);
            var posts = _dataService.GetAuthPosts(id);

            var result = posts.Select(x => new PostDto { Title = x.Title });
            return Ok(result);
        }
    }
}
