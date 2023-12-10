using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Spotify.Streaming.API.Controllers;
using Spotify.Streaming.Application.Interfaces;
using Spotify.Streaming.Domain.Streaming.Aggregates;

namespace Spotify.Streaming.UnitTests.API.Controllers;

[Trait(nameof(PlanController), "")]
public class PlanControllerTests
{
    [Fact]
    public void MustGetPlanByIdSucess()
    {
        // Arrange
        var planExpected = new Plan
        {
            Id = Guid.NewGuid(),
            Name = "Test plan",
            Description = "Test plan description",
            Value = 100
        };

        var planService = new Mock<IPlanService>();

        planService.Setup(x => x.GetPlanById(It.IsAny<Guid>())).Returns(planExpected);

        var instance = new PlanController(planService.Object);

        // Act
        var result = instance.GetPlanById(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<OkObjectResult>();

        result.As<OkObjectResult>().Value.Should().NotBeNull();
        result.As<OkObjectResult>().Value.Should().BeEquivalentTo(planExpected);
    }

    [Fact]
    public void MustGetPlanByIdFail_PlanNotFound()
    {
        // Arrange
        var planService = new Mock<IPlanService>();

        var instance = new PlanController(planService.Object);

        // Act
        var result = instance.GetPlanById(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }
}