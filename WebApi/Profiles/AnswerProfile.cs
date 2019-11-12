using AutoMapper;
using DatabaseService;
using WebApi.Models;

public class AnswerProfile : Profile
{
  public AnswerProfile()
  {
    CreateMap<AnswerDbDto, AnswerDto>();
    // CreateMap<QuestionForCreation, Question>();
  }
}