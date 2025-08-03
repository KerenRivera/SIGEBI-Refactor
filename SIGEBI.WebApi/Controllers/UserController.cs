using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Services;
using SIGEBI.Application.DTOs;
using System.Collections.Generic;

namespace SIGEBI.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UsersDto>> GetAll()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public ActionResult<UsersDto> GetById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null) return NotFound();
            return Ok(user);
            // This method retrieves a user by ID and returns it if found, otherwise returns NotFound.
        }

        [HttpPost]
        public IActionResult Create([FromBody] UsersDto dto)
        {
            _userService.AddUser(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
            // This method creates a new user and returns the created user with a 201 status code.
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateUserDto dto)
        {
            _userService.UpdateUser(id, dto.Name);
            return NoContent();
            // This method updates an existing user by ID and returns NoContent if successful.
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.DeleteUser(id);
            return NoContent();
            // This method deletes a user by ID and returns NoContent if successful.
        }
    }
}
