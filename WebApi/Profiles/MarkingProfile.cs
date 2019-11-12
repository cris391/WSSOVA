using AutoMapper;
using DatabaseService;

public class MarkingProfile : Profile
{
  public MarkingProfile()
  {
    CreateMap<MarkingForCreation, Marking>();
  }
}