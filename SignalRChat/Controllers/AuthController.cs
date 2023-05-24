using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignalRChat.Data;

namespace SignalRChat.Controllers;

public class AuthController: Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpGet("/login")]
    public IActionResult Login()
    {
        return View();
    }
    
    [HttpGet("/register")]
    public IActionResult Register()
    {
        return View();
    }
    
    
    [HttpPost("/login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var signedUser = await _signInManager.UserManager.FindByNameAsync(loginDto.Login);
        var result = await _signInManager.PasswordSignInAsync(signedUser.UserName, loginDto.Password, false,
            lockoutOnFailure: false);


        if (result.Succeeded)
        {
            return Ok();
        }

        return BadRequest();
    }
    
    [HttpPost("/register")]
    public async Task<IActionResult> Register(LoginDto loginDto)
    {
        var user = new IdentityUser()
        {
            UserName = loginDto.Login,
        };
        await _userManager.CreateAsync(user, loginDto.Password);
        var signedUser = await _signInManager.UserManager.FindByNameAsync(loginDto.Login);
        var result = await _signInManager.PasswordSignInAsync(signedUser.UserName, loginDto.Password, false,
            lockoutOnFailure: false);
        
        if (result.Succeeded)
        {
            return Ok();
        }

        return BadRequest();
    }
}