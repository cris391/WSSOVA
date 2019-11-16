using System;
using AutoMapper;
using DatabaseService;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;


namespace WebApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class FullPostController : ControllerBase
  {

    IDataService _dataService;
    IMapper _imapper;
    public FullPostController(IDataService dataService, IMapper imapper)
    {
      _dataService = dataService;
      _imapper = imapper;
    }


    [HttpGet("{questionId}")]
    public ActionResult GetFullPost(int questionId)
    {
      var result = _dataService.GetFullPost(questionId);

      if (result == null) return NotFound();

      return Ok(result);
    }
  }
}
