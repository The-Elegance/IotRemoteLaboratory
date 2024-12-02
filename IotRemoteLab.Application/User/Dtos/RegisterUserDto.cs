using System.ComponentModel.DataAnnotations;

namespace IotRemoteLab.Application.User.Dtos;

public record RegisterUserDto(
    [Required] [EmailAddress]  string Email,
    [Required] string Password, string Name, string Surname, string MiddleName, string University, string UniversityGroup);