﻿using System.ComponentModel.DataAnnotations;

namespace IotRemoteLab.Application.User.Dtos;

public record RegisterUserDto(
    [Required] [EmailAddress]  string Email,
    [Required]  string Password,
    [Required]  string Name,
    [Required]  string Surname,
    string? GroupNumber);