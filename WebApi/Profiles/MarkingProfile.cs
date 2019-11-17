using AutoMapper;
using DatabaseService;
using WebApi.Models;

public class MarkingProfile : Profile
{
  public MarkingProfile()
  {
    CreateMap<MarkingForCreation, Marking>();
    CreateMap<Marking, MarkingDto>();
  }
}