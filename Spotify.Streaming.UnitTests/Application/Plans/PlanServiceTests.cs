using FluentAssertions;
using Moq;
using Spotify.Core.Exceptions;
using Spotify.Streaming.Application.Plans;
using Spotify.Streaming.Domain.Streaming.Aggregates;
using Spotify.Streaming.Infrastructure.Interfaces;

namespace Spotify.Streaming.UnitTests.Application.Plans;

[Trait(nameof(PlanService), "")]
public class PlanServiceTests
{
    [Fact]
    public void MustGetPlanByIdSucess()
    {
        // Arrange
        var planExpected = new Plan
        {
            Id = Guid.NewGuid(),
            Name = "Test name",
            Description = "Test description",
            Value = 100
        };

        var planRepository = new Mock<IPlanRepository>();

        planRepository.Setup(x => x.GetPlanById(It.IsAny<Guid>())).Returns(planExpected);

        var instance = new PlanService(planRepository.Object);

        // Act
        var result = instance.GetPlanById(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<Plan>();
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(planExpected);
    }

    [Fact]
    public void MustGetPlanByIdFail_PlanNotFound()
    {
        // Arrange
        var planRepository = new Mock<IPlanRepository>();

        var instance = new PlanService(planRepository.Object);

        // Act
        var result = instance.GetPlanById(Guid.NewGuid());

        // Assert
        result.Should().BeNull();
    }
}