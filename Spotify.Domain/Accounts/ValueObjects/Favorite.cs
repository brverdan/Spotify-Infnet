namespace Spotify.Domain.Accounts.ValueObjects;
public class Favorite
{
    public Guid Id { get; set; }
    public Guid IdFavorite { get; set; }
    public string Name { get; set; }
    public Category CategoryType { get; set; }
}