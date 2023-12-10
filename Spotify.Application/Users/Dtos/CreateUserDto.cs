using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Spotify.Application.Users.Dtos;

public class CreateUserDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Cpf { get; set; }

    [Required]
    public Guid Plan { get; set; }

    [Required]
    public CreditCardDto CreditCard { get; set; }
}