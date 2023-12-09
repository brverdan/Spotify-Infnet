using System.ComponentModel.DataAnnotations;

namespace Spotify.Application.Users.Dtos;

public class CreditCardDto
{
    [Required]
    public string Number { get; set; }

    [Required]
    public double AvailableLimit { get; set; }

    [Required]
    public bool ActiveCard { get; set; }

    [Required]
    public int SecureCode { get; set; }
}