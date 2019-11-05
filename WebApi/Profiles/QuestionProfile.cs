using AutoMapper;
using DatabaseService;
using WebApi.Models;

public class QuestionProfile : Profile
{
  public QuestionProfile()
  {
    CreateMap<Question, QuestionDto>();
  }
}