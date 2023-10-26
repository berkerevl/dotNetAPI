using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotNetAPI.Users.Services;
using DotNetAPI.Users.Dtos;

namespace DotNetAPI.Users.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;

        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        public async Task<ActionResult> AddUser([FromBody] CreateUserDto createUserDto)
        {
            var user = await _usersService.AddUser(createUserDto);
            return CreatedAtAction(nameof(GetUserById), new { userId = user.UserId }, user);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult> GetUserById(string userId)
        {
            var user = await _usersService.GetUserById(userId);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] UpdateUserDto updateUserDto)
        {
            try
            {
                updateUserDto.UserId = userId;
                await _usersService.UpdateUser(updateUserDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            try
            {
                await _usersService.SoftDeleteUser(userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{userId}/money")]
        public async Task<IActionResult> GetUserMoney(string userId)
        {
            try
            {
                var money = await _usersService.GetUserMoney(userId);
                return Ok(money);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
