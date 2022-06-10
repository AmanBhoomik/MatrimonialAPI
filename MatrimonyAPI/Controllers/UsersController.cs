using MatrimonyAPI.Models;
using MatrimonyAPI.Models.Entities;
using MatrimonyAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrimonyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _IUser;

        public UsersController(IUserService IUser)
        {
            _IUser = IUser;
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await Task.FromResult(_IUser.GetAllUser());
        }
        [HttpGet("Email")]
        public async Task<ActionResult<IEnumerable<string>>> GetEmail()
        {
            return await Task.FromResult(_IUser.GetAllUserEmail());
        }

        [HttpPost("Search")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers(SearchModel model)
        {
            return await Task.FromResult(_IUser.GetAllUserByCondition(model));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await Task.FromResult(_IUser.GetUser(id));
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        
        [HttpPost("Register")]
        public async Task<ActionResult<User>> Post(RegisterUserModel model)
        {
            User user= _IUser.RegisterUser(model);
            return await Task.FromResult(user);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            _IUser.DeleteUser(id);
            return Ok();
        }

        [HttpPut("UpdateBasicDetail")]
        public async Task<ActionResult<User>> UpdateBasicProfile([FromForm] BasicProfileModel model)
        {
            User user = _IUser.UpdateBasicInfo(model);
            return await Task.FromResult(user);
        }


        [HttpPut("UpdateProfessionalDetail")]
        public async Task<ActionResult<User>> UpdateProfessionalProfile(ProfessionalProfileModel model)
        {
            User user = _IUser.UpdateProfessionalDetail(model);
            return await Task.FromResult(user);
        }

        [HttpPost("Authenticate")]
        public IActionResult Authenticate(LoginUserModel model)
        {
            var response = _IUser.AuthenticateUser(model);
            
            return Ok(response);
        }



    }
}
