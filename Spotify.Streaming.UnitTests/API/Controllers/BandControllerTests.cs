using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Spotify.Streaming.API.Controllers;
using Spotify.Streaming.Application.Bands.Dtos;
using Spotify.Streaming.Application.Bands.Dtos.Response;
using Spotify.Streaming.Application.Interfaces;

namespace Spotify.Streaming.UnitTests.API.Controllers;

[Trait(nameof(BandController), "")]
public class BandControllerTests
{
    [Fact]
    public void MustGetBandByIdSucess()
    {
        // Arrange
        var bandExpected = new ResponseBandDto
        {
            Id = Guid.NewGuid(),
            Name = "Test band",
            Albums = new List<ResponseAlbumDto>
            {
                new ResponseAlbumDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Test album",
                    ReleaseYear = DateTime.Now,
                    Musics = new List<ResponseMusicDto>
                    {
                        new ResponseMusicDto
                        {
                            Id = Guid.NewGuid(),
                            Name = "Test music",
                            ReleaseYear = DateTime.Now,
                            Duration = 100
                        }
                    }
                }
            }
        };

        var bandService = new Mock<IBandService>();

        bandService.Setup(x => x.GetBandById(It.IsAny<Guid>())).Returns(bandExpected);

        var instance = new BandController(bandService.Object);

        // Act
        var result = instance.GetBandById(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<OkObjectResult>();

        result.As<OkObjectResult>().Value.Should().NotBeNull();
        result.As<OkObjectResult>().Value.Should().BeEquivalentTo(bandExpected);
    }

    [Fact]
    public void MustGetBandByIdFail_BandNotFound()
    {
        // Arrange
        var bandService = new Mock<IBandService>();

        var instance = new BandController(bandService.Object);

        // Act
        var result = instance.GetBandById(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public void MustCreateBandSucess()
    {
        // Arrange
        var bandExpected = new ResponseBandDto
        {
            Id = Guid.NewGuid(),
            Name = "Test band",
            Albums = new List<ResponseAlbumDto>
            {
                new ResponseAlbumDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Test album",
                    ReleaseYear = DateTime.Now,
                    Musics = new List<ResponseMusicDto>
                    {
                        new ResponseMusicDto
                        {
                            Id = Guid.NewGuid(),
                            Name = "Test music",
                            ReleaseYear = DateTime.Now,
                            Duration = 100
                        }
                    }
                }
            }
        };

        var bandDto = new CreateBandDto
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

        var bandService = new Mock<IBandService>();

        bandService.Setup(x => x.CreateBand(bandDto)).Returns(bandExpected);

        var instance = new BandController(bandService.Object);

        // Act
        var result = instance.CreateBand(bandDto);

        // Assert
        result.Should().BeOfType<CreatedResult>();

        result.As<CreatedResult>().Value.Should().NotBeNull();
        result.As<CreatedResult>().Value.Should().BeEquivalentTo(bandExpected);
    }

    [Fact]
    public void MustCreateBandFail_ModelInvalid()
    {
        // Arrange
        var bandDto = new CreateBandDto
        {
            Albums = new List<CreateAlbumDto>
            {
                new CreateAlbumDto
                {
                    Musics = new List<CreateMusicDto>
                    {
                        new CreateMusicDto()
                    }
                }
            }
        };

        var bandService = new Mock<IBandService>();

        var instance = new BandController(bandService.Object);

        instance.ModelState.AddModelError("", "Invalid dto");

        // Act
        var result = instance.CreateBand(bandDto);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public void MustGetMusicByIdSucess()
    {
        // Arrange
        var musicExpected = new ResponseMusicDto
        {
            Id = Guid.NewGuid(),
            Name = "Test music",
            ReleaseYear = DateTime.Now,
            Duration = 100
        };

        var bandService = new Mock<IBandService>();

        bandService.Setup(x => x.GetMusicById(It.IsAny<Guid>())).Returns(musicExpected);

        var instance = new BandController(bandService.Object);

        // Act
        var result = instance.GetMusicById(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<OkObjectResult>();

        result.As<OkObjectResult>().Value.Should().NotBeNull();
        result.As<OkObjectResult>().Value.Should().BeEquivalentTo(musicExpected);
    }

    [Fact]
    public void MustGetMusicByIdFail_MusicNotFound()
    {
        // Arrange
        var bandService = new Mock<IBandService>();

        var instance = new BandController(bandService.Object);

        // Act
        var result = instance.GetMusicById(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }
}