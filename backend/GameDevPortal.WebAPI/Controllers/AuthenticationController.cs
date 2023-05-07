using AutoMapper;
using GameDevPortal.Core.Entities;
using GameDevPortal.Core.Interfaces.Authentication;
using GameDevPortal.Core.Models;
using GameDevPortal.WebAPI.Models.Dtos.UserDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace ConsoleProjectManagement.WebAPI.Controllers;

[ApiController]
[Route("api/authentication"), Authorize]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authManager;
    private readonly ILogger _logger;

    public AuthenticationController(IAuthenticationService authManager, ILogger logger)
    {
        _authManager = authManager ?? throw new ArgumentNullException(nameof(authManager));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPost("login"), AllowAnonymous]
    public async Task<ActionResult> Authenticate([FromBody] UserAuthenticationDto userDto)
    {
        var authenticationResult = await _authManager.ValidateUser(userDto.UserName, userDto.Password);
        if (!authenticationResult.Success)
        {
            _logger.LogError($"Authentication failed, message: {authenticationResult.ErrorMessage}");
            return Forbid();
        }

        User authenticatedUser = authenticationResult.ResultData;

        var tokenGetResult = await _authManager.CreateToken(authenticatedUser);

        if (!tokenGetResult.Success)
        {
            _logger.LogError($"Getting token failed, message: {tokenGetResult.ErrorMessage}");
            throw tokenGetResult.Exception;
        }

        TokenDto token = tokenGetResult.ResultData;

        return Ok(new { Token = token });
    }

    [HttpPost("logout")]
    public ActionResult Logout()
    {
        return Ok(new { Token = _authManager.CreateExpiredToken() });
    }    
}