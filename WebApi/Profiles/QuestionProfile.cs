using AutoMapper;
using DatabaseService;
using WebApi.Models;

public class QuestionProfile : Profile
{
  public QuestionProfile()
  {
    CreateMap<Question, QuestionDto>();
    CreateMap<QuestionForCreation, Question>();
    CreateMap<QuestionDbDto, QuestionDto>();
  }
}