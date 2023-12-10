using FluentAssertions;
using FluentAssertions.Extensions;
using Spotify.Core.Exceptions;
using Spotify.Streaming.Domain.Streaming.Aggregates;
using Spotify.Streaming.Domain.Streaming.Exception;

namespace Spotify.Streaming.UnitTests.Domain.Streaming.Aggregates;

[Trait(nameof(Music), "")]
public class MusicTests
{
    [Fact]
    public void MustCreateMusicSucess()
    {
        // Arrange
        var music = new Music();

        var name = "Test music";
        var releaseYear = DateTime.Now;
        var duration = 100;

        // Act
        music.Create(name, releaseYear, duration);

        // Assert
        music.Id.Should().NotBeEmpty();
        music.Name.Should().NotBeNullOrWhiteSpace().And.Be(name);
        music.ReleaseYear.Should().BeCloseTo(releaseYear, 2.Seconds());
        music.Duration.Should().NotBeNull();
        music.Duration.Value.Should().NotBe(0).And.Be(duration);
    }

    [Fact]
    public void MustCreateMusicFail_MusicDurationNegative()
    {
        // Arrange
        var music = new Music();

        var name = "Test music";
        var releaseYear = DateTime.Now;
        var duration = -100;

        // Act
        var result = () => music.Create(name, releaseYear, duration);

        // Assert
        result.Should().Throw<DurationException>()
            .And.Errors[0]
            .ErrorMessage.Should().Contain("Music cannot have negative duration");
    }
}