using Microsoft.AspNetCore.Mvc;
using BackEnd.Dtos.User;
using BackEnd.Services.Interfaces;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        // 构造函数注入业务层 
        public UserController(IUserService service)
        {
            _service = service;
        }

        // 配置API接口，这样前端调用接口的时候的格式应该是  '[地址]:[端口]/api/User/GetAll'
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _service.GetAllUsersAsync();
            return Ok(users);
        }

        // '[地址]:[端口]/api/User/{id}'
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _service.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // '[地址]:[端口]/api/User/Add'
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
        {
            var user = await _service.CreateUserAsync(dto);
            return CreatedAtAction(nameof(GetUserById), new { id = user.UserID }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto dto)
        {
            var success = await _service.UpdateUserAsync(id, dto);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var success = await _service.DeleteUserAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
