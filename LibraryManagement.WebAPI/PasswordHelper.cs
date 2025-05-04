using System;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagement.WebAPI;

public class PasswordHelper
{
    private readonly PasswordHasher<IdentityUser> _passwordHasher = new PasswordHasher<IdentityUser>();

    public string HashPassword(string password)
    {
        return _passwordHasher.HashPassword(new IdentityUser(), password);
    }

}
