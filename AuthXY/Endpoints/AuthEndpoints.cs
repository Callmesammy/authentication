
using AuthXY.Service;
using AuthXY.Dtos;
using AuthXY.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthXY.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder routes)
    {
        // Register
        routes.MapPost("/api/register", async (
            RegisterDto dto,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager) =>
        {
            var user = new User
            {
                FullName = dto.Fullname,
                Email = dto.Email,
                UserName = dto.Email
            };

            var result = await userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return Results.BadRequest(result.Errors);

            if (!await roleManager.RoleExistsAsync("user"))
                await roleManager.CreateAsync(new IdentityRole("user"));

            await userManager.AddToRoleAsync(user, "user");
            return Results.Ok(new { Message = "User registered successfully" });
        });

        // Login
        routes.MapPost("/api/login", async (
            LoginDto dto,
            UserManager<User> userManager,
            TokenService tokenService) =>
        {
            var user = await userManager.FindByEmailAsync(dto.Fullname);
            if (user is null || !await userManager.CheckPasswordAsync(user, dto.Password))
                return Results.Unauthorized();

            var roles = await userManager.GetRolesAsync(user);
            var token = tokenService.CreateToken(user, roles);
            return Results.Ok(new { Token = token });
        });
    }
}
