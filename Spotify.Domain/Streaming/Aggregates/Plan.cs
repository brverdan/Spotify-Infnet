namespace Spotify.Domain.Streaming.Aggregates;

public class Plan
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Value { get; set; }
}