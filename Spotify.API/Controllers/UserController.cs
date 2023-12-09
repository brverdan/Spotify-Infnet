using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Users;

namespace Spotify.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    public IUserService UserService { get; set; }

    public UserController(IUserService userService)
    {
        UserService = userService;
    }

    [HttpGet("{id}")]
    public IActionResult GetUserById([FromRoute] Guid id)
    {
        var user = UserService.GetUserById(id);
        
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost("create")]
    public IActionResult CreateUser([FromBody] CreateUserDto userCreateDto)
    {
        if(!ModelState.IsValid) 
        {
            return BadRequest(ModelState);
        }

        var result = UserService.CreateUser(userCreateDto);

        return Created("", result);
    }
}