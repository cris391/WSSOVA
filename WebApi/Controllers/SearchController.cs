using System.Text.RegularExpressions;
using AutoMapper;
using DatabaseService;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

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

      return Ok(CreateSearchResultDtos(result));
    }

    [Authorize]
    [HttpGet("history")]
    public ActionResult GetSearchHistory()
    {
      var userId = Helpers.GetUserIdFromJWTToken(Request.Headers["Authorization"]);

      var result = _dataService.GetSearchHistory(userId);

      if (result.Count == 0) return NoContent();

      return Ok(CreateSearchHistoryDtos(result));
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

    ///////////////////
    //
    // Helpers
    //
    //////////////////////

    private List<SearchResultDto> CreateSearchResultDtos(List<SearchResult> searchResults)
    {
      List<SearchResultDto> searchResultDtos = new List<SearchResultDto>();
      foreach (var searchResult in searchResults)
      {
        searchResultDtos.Add(new SearchResultDto
        {
          Id = searchResult.QuestionId,
          Link = Url.Link(
              nameof(QuestionsController.GetQuestion),
              new { questionId = searchResult.QuestionId }),
          LinkPost = Url.Link(
              nameof(QuestionsController.GetFullPost),  
              new { questionId = searchResult.QuestionId }),
          Title = searchResult.Title
        });
      };
      return searchResultDtos;
    }

    private List<SearchHistoryDto> CreateSearchHistoryDtos(List<SearchHistory> searchHistories)
    {
      List<SearchHistoryDto> searchHistoryDtos = new List<SearchHistoryDto>();
      foreach (var searchHistory in searchHistories)
      {
        searchHistoryDtos.Add(new SearchHistoryDto
        {
          SearchDate = searchHistory.SearchDate,
          QueryText = searchHistory.QueryText
        });
      };
      return searchHistoryDtos;
    }
  }
}