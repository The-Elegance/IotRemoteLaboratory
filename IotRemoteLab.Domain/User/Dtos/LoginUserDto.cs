using System.ComponentModel.DataAnnotations;

namespace IotRemoteLab.Domain.User.Dtos;

public record LoginUserDto(
    [Required] [EmailAddress]  string Email,
    [Required]  string Password);