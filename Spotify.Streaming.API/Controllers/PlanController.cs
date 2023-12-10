using Microsoft.AspNetCore.Mvc;
using Spotify.Streaming.Application.Interfaces;

namespace Spotify.Streaming.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlanController : ControllerBase
{
    private IPlanService PlanService { get; set; }

    public PlanController(IPlanService planService)
    {
        PlanService = planService;
    }

    [HttpGet("{id}")]
    public IActionResult GetPlanById([FromRoute] Guid id)
    {
        var plan = PlanService.GetPlanById(id);

        if (plan == null)
        {
            return NotFound();
        }

        return Ok(plan);
    }
}
