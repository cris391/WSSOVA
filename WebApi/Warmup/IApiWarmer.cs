using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public interface IApiWarmer
{
  Task WarmUp<TStartup>(IServiceProvider serviceProvider, IUrlHelper urlHelper, HttpRequest request, params string[] ignoredControllers);
}
