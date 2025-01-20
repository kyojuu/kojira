using kojira.WebApi.Contracts;
using kojira.WebApi.Core.Bus;
using kojira.WebApi.Identity.Data;
using kojira.WebApi.Identity.Models;
using kojira.WebApi.Identity.Models.AccountViewModels;
using kojira.WebApi.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace kojira.WebApi.Controller.Account;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        // Sign In
        var signInResult = await _signInManager.PasswordSignInAsync(model.
            Email, model.Password, false, true);
        if (signInResult.Succeeded)
        {
            return Ok("Login successful.");
        }

        // Get User
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return BadRequest();
        }
        //_logger.LogInformation(1 ,"User logged in.");
        return Unauthorized("Invalid login attempt.");
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        // Add User
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email, };
        var identityResult = await _userManager.CreateAsync(user, model.Password);
        if (!identityResult.Succeeded)
        {
            return BadRequest(identityResult.Errors);
        }

        return Ok("Registration successful.");
    }
}
