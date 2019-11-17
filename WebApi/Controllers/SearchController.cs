using System;
using System.Text.RegularExpressions;
using AutoMapper;
using DatabaseService;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class SearchController : ControllerBase
  {
    IDataService _dataService;
    private IMapper _mapper;
    public SearchController(IDataService dataService, IMapper mapper)
    {
      _dataService = dataService;
      _mapper = mapper;
    }

    [Authorize]
    [HttpGet]
    public ActionResult Search([FromQuery] SearchQuery searchQuery)
    {
      var words = GetWords(searchQuery.Q);

      var userId = Helpers.GetUserIdFromJWTToken(Request.Headers["Authorization"]);

      var result = _dataService.Search(words, userId);

      if (result.Count == 0) return NoContent();

      return Ok(result);
    }

    [Authorize]
    [HttpGet("history")]
    public ActionResult GetSearchHistory()
    {
      var userId = Helpers.GetUserIdFromJWTToken(Request.Headers["Authorization"]);

      var result = _dataService.GetSearchHistory(userId);

      if (result.Count == 0) return NoContent();

      return Ok(result);
    }

    ///////////////////
    //
    // Helpers
    //
    ///////////////////

    public string[] GetWords(string values)
    {
      // values = "i+want+a+banana+and+a+half'-";

      // clean input but retain + symbol
      var fixedInput = Regex.Replace(values, "[^a-zA-Z0-9+% ._]", string.Empty);
      // split on + symbol
      var words = fixedInput.Split(' ');

      return words;
    }
  }
}