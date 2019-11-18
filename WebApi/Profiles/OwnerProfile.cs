using AutoMapper;
using DatabaseService;
using WebApi.Models;

public class OwnerProfile : Profile
{
  public OwnerProfile()
  {
    CreateMap<Owner, OwnerDto>();
  }
}