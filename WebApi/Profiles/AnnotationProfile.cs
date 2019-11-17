using AutoMapper;
using DatabaseService;

public class AnnotationProfile : Profile
{
  public AnnotationProfile()
  {
    CreateMap<Annotation, AnnotationDto>();
  }
}