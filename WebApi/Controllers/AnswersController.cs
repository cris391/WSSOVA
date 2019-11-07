using System;
using System.Collections.Generic;
using AutoMapper;
using DatabaseService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
  [ApiController]
  [Route("api/answers")]
  public class AnswersController : ControllerBase
  {
    IDataService _dataService;
    private IMapper _mapper;
    public AnswersController(IDataService dataService, IMapper mapper)
    {
      _dataService = dataService;
      _mapper = mapper;
    }

    [HttpGet]
    public ActionResult GetAnswers()
    {
      var result = _dataService.GetAnswers();

      return Ok(result);
    }

    // [HttpGet("{questionId}")]
    // public ActionResult GetAnswersForQuestion(int questionId)
    // {
    //   var result = _dataService.GetAnswersForQuestion(questionId);

    //   return Ok(result);
    // }
  }

  
}