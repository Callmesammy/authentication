

using Microsoft.AspNetCore.Identity;
namespace AuthXY.Models;

public class User : IdentityUser

{
    public string FullName { get; set; } = string.Empty;
}