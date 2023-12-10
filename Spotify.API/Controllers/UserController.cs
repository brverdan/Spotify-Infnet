using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Interfaces;
using Spotify.Application.Users.Dtos;

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

    [HttpPost("createUser")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userCreateDto)
    {
        if(!ModelState.IsValid) 
        {
            return BadRequest(ModelState);
        }

        var result = await UserService.CreateUser(userCreateDto);

        return Created("", result);
    }

    [HttpPost("{id}/playlist/create")]
    public IActionResult CreatePlaylist([FromRoute] Guid id, [FromBody] PlaylistDto playlistDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = UserService.CreatePlaylist(id, playlistDto);

        return Created("", result);
    }

    [HttpPost("{userId}/playlist/{playlistId}/addMusic")]
    public async Task<IActionResult> AddMusic([FromRoute] Guid playlistId, [FromBody] AddMusicDto addMusicDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await UserService.AddMusic(playlistId, addMusicDto.MusicId);

        return Created("", result);
    }

    [HttpGet("{userId}/playlist/{playlistId}")]
    public IActionResult GetPlaylistById([FromRoute] Guid playlistId)
    {
        var result = UserService.GetPlaylistById(playlistId);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}