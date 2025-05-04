using System;
using LibraryManagement.WebAPI.Entity;

namespace LibraryManagement.WebAPI;

public interface IToken
{
    public string GenerateJwtToken(UserModel user); // Overloaded method for UserModel

}
