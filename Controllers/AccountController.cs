using BlogAspNet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace BlogAspNet.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    [HttpGet("v1/login")]
    public IActionResult Login([FromServices] TokenService tokenService)
    {
        var token = tokenService.GenerateToken(null);

        return Ok(token);
    }
    
    [Authorize(Roles = "user, admin")]
    [HttpGet("v1/User")]
    public IActionResult GetUser() => Ok(User.Identity.Name);
    
    [Authorize(Roles = "author, admin")]
    [HttpGet("v1/Author")]
    public IActionResult GetAuthor() => Ok(User.Identity.Name);
    
    [Authorize(Roles = "admin")]
    [HttpGet("v1/Admin")]
    public IActionResult GetAdmin() => Ok(User.Identity.Name);
    
}