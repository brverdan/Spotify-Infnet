using Microsoft.AspNetCore.Mvc;
using Spotify.Streaming.Application.Bands.Dtos;
using Spotify.Streaming.Application.Interfaces;

namespace Spotify.Streaming.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BandController : ControllerBase
{
    private IBandService BandService { get; set; }

    public BandController(IBandService bandService)
    {
        BandService = bandService;
    }

    [HttpPost("Create")]
    public IActionResult CreateBand([FromBody] CreateBandDto createBandDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = BandService.CreateBand(createBandDto);

        return Created("", result);
    }

    [HttpGet("music/{musicId}")]
    public IActionResult GetMusicById([FromRoute] Guid musicId)
    {
        var music = BandService.GetMusicById(musicId);

        if (music == null)
        {
            return NotFound();
        }

        return Ok(music);
    }
}