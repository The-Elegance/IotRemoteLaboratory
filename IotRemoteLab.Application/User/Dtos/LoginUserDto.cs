using System.ComponentModel.DataAnnotations;

namespace IotRemoteLab.Application.User.Dtos;

public record LoginUserDto(
    [Required] [EmailAddress]  string Email,
    [Required]  string Password);