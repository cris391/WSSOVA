using AutoMapper;
using DatabaseService;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route ("api/comments")]
    public class CommentController: ControllerBase
    {
        IDataService _dataService;
        private IMapper _mapper;
        public CommentController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper; 
        }

        [HttpGet]
        [Route("post/{postId}")] // Name = nameof(GetComments))
        public ActionResult GetComments(int postId)
        {
            var commentResult = _dataService.GetComments(postId);

            return Ok(commentResult);
        }
    }
}
