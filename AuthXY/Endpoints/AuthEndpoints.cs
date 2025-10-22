using AuthXY.Dtos;
using AuthXY.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthXY.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder routes)
    {
        // REGISTER 
        // here is for the registrer segment
        routes.MapPost("/api/register", async (
            RegisterDto dto,
            UserManager < User > UserManager,
            RoleManager<IdentityRole> RoleManager) =>
        {
            var user = new User
            {
                FullName = dto.Fullname,
                Email = dto.Email,
                UserName = dto.Email
            };

            var result = await UserManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return Results.BadRequest(result.Errors);

            if (!await RoleManager.RoleExistsAsync("user"))
                await RoleManager.CreateAsync(new IdentityRole("user"));

            await UserManager.AddToRoleAsync(user, "user");
            return Results.Ok(new { Message = "User Registrered Successfully" });
   
   
        ??
   
        }


        ))
    }
}