using AutoMapper;
using DatabaseService;
using WebApi.Models;

public class SearchResultProfile : Profile
{
  public SearchResultProfile()
  {
    CreateMap<SearchResult, SearchResultDto>();
  }
}