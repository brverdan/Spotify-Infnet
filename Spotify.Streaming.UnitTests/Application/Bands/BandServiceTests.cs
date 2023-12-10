using FluentAssertions;
using Moq;
using Spotify.Core.Exceptions;
using Spotify.Streaming.Application.Bands;
using Spotify.Streaming.Application.Bands.Dtos;
using Spotify.Streaming.Application.Bands.Dtos.Response;
using Spotify.Streaming.Domain.Streaming.Aggregates;
using Spotify.Streaming.Domain.Streaming.Exception;
using Spotify.Streaming.Infrastructure.Interfaces;

namespace Spotify.Streaming.UnitTests.Application.Bands;

[Trait(nameof(BandService), "")]
public class BandServiceTests
{
    [Fact]
    public void MustCreateBandSuccess()
    {
        // Arrange
        var createBandDto = new CreateBandDto
        {
            Name = "Test band",
            Albums = new List<CreateAlbumDto>
            {
                new CreateAlbumDto
                {
                    Name = "Test album",
                    ReleaseYear = DateTime.Now,
                    Musics = new List<CreateMusicDto>
                    {
                        new CreateMusicDto
                        {
                            Name = "Test music",
                            ReleaseYear = DateTime.Now,
                            Duration = 100
                        }
                    }
                }
            }
        };

        var bandRepository = new Mock<IBandRepository>();

        bandRepository.Setup(x => x.CreateBand(It.IsAny<Band>()));

        var instance = new BandService(bandRepository.Object);

        // Act
        var result = instance.CreateBand(createBandDto);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ResponseBandDto>();
    }

    [Fact]
    public void MustCreateBandFail_MusicDurationInvalid()
    {
        // Arrange
        var createBandDto = new CreateBandDto
        {
            Name = "Test band",
            Albums = new List<CreateAlbumDto>
            {
                new CreateAlbumDto
                {
                    Name = "Test album",
                    ReleaseYear = DateTime.Now,
                    Musics = new List<CreateMusicDto>
                    {
                        new CreateMusicDto
                        {
                            Name = "Test music",
                            ReleaseYear = DateTime.Now,
                            Duration = -100
                        }
                    }
                }
            }
        };

        var bandRepository = new Mock<IBandRepository>();

        bandRepository.Setup(x => x.CreateBand(It.IsAny<Band>()));

        var instance = new BandService(bandRepository.Object);

        // Act
        var result = () => instance.CreateBand(createBandDto);

        // Assert
        result.Should().Throw<DurationException>()
            .And.Errors[0]
            .ErrorMessage.Should().Contain("Music cannot have negative duration"); ;
    }
}