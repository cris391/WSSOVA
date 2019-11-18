using AutoMapper;
using DatabaseService;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class OwnersController : ControllerBase
  {
    IDataService _dataService;
    IMapper _mapper;
    public OwnersController(IDataService dataService, IMapper mapper)
    {
      _dataService = dataService;
      _mapper = mapper;
    }

    [HttpGet("{ownerId}", Name = nameof(GetOwner))]
    public ActionResult GetOwner(int ownerId)
    {
      var result = _dataService.GetOwner(ownerId);

      return Ok(CreateOwnerDto(result));
    }

    ///////////////////
    //
    // Helpers
    //
    ///////////////////

    private OwnerDto CreateOwnerDto(Owner owner)
    {
      var dto = _mapper.Map<OwnerDto>(owner);
      dto.Link = Url.Link(
              nameof(GetOwner),
              new { ownerId = owner.UserId });
      return dto;
    }

  }

}
