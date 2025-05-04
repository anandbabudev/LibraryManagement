using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.WebAPI.Modal;

public class LoginModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}