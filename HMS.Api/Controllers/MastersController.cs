using HMS.BL.Interfaces.Masters;
using HMS.Entity.Masters.titles.Request;
using HMS.Entity.Masters.titles.Response;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MastersController : ControllerBase
    {
        public ITitleBL TitleDetails { get; set; }
        private readonly ITitleBL _titleDetails;
        public MastersController(ITitleBL titleDetails)
        {
            _titleDetails = titleDetails;

        }

        // GET: api/<UserController>
        [HttpGet]
        //[Authorize]
        [Route("GetTitleDetails")]
        public async Task<List<TitleResponse>> GetUserDetails()
        {
            return await _titleDetails.GetTitleDetails();
        }

        // GET: api/<UserController>
        [HttpPost]
        //[Authorize]
        [Route("GetTitleDetailsByID")]
        public async Task<List<TitleResponse>> GetTitleDetailsByID(TitleRequest titleRequest)
            {
            return await _titleDetails.GetTitleDetailsByID(titleRequest);
            }

        [HttpPost]
        //[Authorize]
        [Route("SaveTitle")]
        public async Task<TitleResponse> SaveTitle(TitleSaveRequest titleSaveRequest)
        {

            return await _titleDetails.SaveTitle(titleSaveRequest);
        }

        
        }
}
