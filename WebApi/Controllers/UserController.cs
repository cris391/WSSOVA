﻿using DatabaseService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
  [ApiController]
  [Route("api/users")]
  public class UserController : ControllerBase
  {
    IDataService _dataService;
    public UserController(IDataService dataService)
    {
      _dataService = dataService;
    }

    [Route("create")]
    [HttpPost]
    public User CreateUser([FromBody] string username, string password, string salt)
    {
      var newUser = _dataService.CreateUser(username, password, salt);
      return newUser;
    }
  }
}
