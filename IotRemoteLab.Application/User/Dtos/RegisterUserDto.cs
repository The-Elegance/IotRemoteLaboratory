using System.ComponentModel.DataAnnotations;

namespace IotRemoteLab.Application.User.Dtos;

public record RegisterUserDto(
    [Required] [EmailAddress]  string Email,
    [Required] string Password, 
    string Name, 
    string Surname, 
    string MiddleName, 
    Guid UniversityId, 
    Guid AcademyGroupId, 
    string? keyword);