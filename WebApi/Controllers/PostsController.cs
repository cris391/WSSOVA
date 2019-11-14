using System.Collections.Generic;
using AutoMapper;
using DatabaseService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
  [ApiController]
  [Route("api/posts")]
  public class PostsController : ControllerBase
  {
    IDataService _dataService;
    private IMapper _mapper;
    public PostsController(IDataService dataService, IMapper mapper)
    {
      _dataService = dataService;
      _mapper = mapper;
    }

    [HttpGet]
    public ActionResult GetPosts()
    {
      var result = _dataService.GetPosts();
      return Ok(result);
    }
  }
}