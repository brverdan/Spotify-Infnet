using Spotify.Domain.Streaming.ValueObjects;

namespace Spotify.Domain.Aggregates;

public class Music
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Duration Duration { get; set; }
    public DateTime CreationYear { get; set; }
    public string Album { get; set;}
}