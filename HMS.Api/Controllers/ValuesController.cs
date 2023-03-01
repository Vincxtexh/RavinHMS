using Microsoft.AspNetCore.Mvc;
using HMS.BL;
using HMS.Entity;
using HMS.Entity.Request;
using HMS.Entity.Response;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public IUserDetails UserDetails { get; set; }
        private readonly IUserDetails _userDetails;

        
     
        public ValuesController(IUserDetails userDetails)
        {
            _userDetails = userDetails;
          
        }

        // GET: api/<UserController>
        [HttpGet]
        //[Authorize]
        [Route("GetUserDetails")]
        public async Task<List<UserResponse>> GetUserDetails()
        {
            return await _userDetails.GetUserDetails();
        }

        [HttpGet]
        //[Authorize]
        [Route("GetUserDetailsById/{Id}")]
        public async Task<UserResponse> GetUserDetailsById(int Id)
        {
          

            UserResponse userResponse = new UserResponse();


            userResponse = await _userDetails.GetUserDetailsById(Id);
               
             
            return userResponse;
        }


        [HttpDelete]
        //[Authorize]
        [Route("DeleteUserDetailsById/{Id}")]
        public async Task<bool> Delete(int Id)
        {
            return await _userDetails.DeleteUserDetailsById(Id);
        }


        [HttpPost]
        //[Authorize]
        [Route("CreateCRMSUser")]
        public async Task<CreateUserResponse> CreateCRMSUser(CreateUserRequest createUserRequest)
        {

            return await _userDetails.CreateCRMSUser(createUserRequest);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }



        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

    }
}
