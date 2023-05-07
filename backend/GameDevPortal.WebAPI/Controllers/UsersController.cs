using AutoMapper;
using GameDevPortal.Core.Entities;
using GameDevPortal.Core.Interfaces.Authentication;
using GameDevPortal.Core.Models;
using GameDevPortal.WebAPI.Extensions.EntityExtensions;
using GameDevPortal.WebAPI.Models.Dtos.UserDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace ConsoleProjectManagement.WebAPI.Controllers;

[ApiController]
[Route("users"), Authorize]
public class UsersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public UsersController(IMapper mapper, IUserService userService)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    [HttpGet, Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<UserGetDto>>> List([FromQuery][Optional] Pagination pagination, CancellationToken cancellationToken = default)
    {
        var listResult = await _userService.List(pagination, cancellationToken);

        if (!listResult.Success) throw listResult.Exception;

        IEnumerable<User> users = listResult.ResultData;
        List<UserGetDto> userDtos = new();

        foreach (User u in users)
        {
            var getRolesResult = await _userService.GetRoles(u);

            if (!getRolesResult.Success) throw getRolesResult.Exception;

            IEnumerable<string> roles = getRolesResult.ResultData;

            UserGetDto userDto = _mapper.Map<UserGetDto>(u);
            userDto.Roles = roles;

            userDtos.Add(userDto);
        }

        return Ok(userDtos);
    }

    [HttpPost, Authorize(Roles = "Administrator")]
    public async Task<ActionResult> Create([FromBody] UserCreationDto dto)
    {
        User user = _mapper.Map<User>(dto);

        var createResult = await _userService.Create(user, dto.Password, dto.Roles);

        if (!createResult.Success) throw createResult.Exception;

        return StatusCode(201);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<UserGetDto>>> Get(Guid id)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
        var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        if (id.ToString() != userId && userRole != "Administrator")
        {
            return Unauthorized("You are not authorized to access this account's information.");
        }

        var getResult = await _userService.Get(id);

        if (!getResult.Success && getResult.Exception is KeyNotFoundException) return NotFound();
        if (!getResult.Success) throw getResult.Exception;

        User user = getResult.ResultData;

        var getRolesResult = await _userService.GetRoles(user);

        if (!getRolesResult.Success) throw getRolesResult.Exception;

        IEnumerable<string> roles = getRolesResult.ResultData;

        UserGetDto userDto = _mapper.Map<UserGetDto>(user);
        userDto.Roles = roles;

        return Ok(userDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UserUpdateDto dto)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
        var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        if (id.ToString() != userId && userRole != "Administrator")
        {
            return Unauthorized("You are not authorized to make changes to this account.");
        }

        var getResult = await _userService.Get(id);

        if (!getResult.Success && getResult.Exception is KeyNotFoundException) return NotFound();
        if (!getResult.Success) throw getResult.Exception;

        User user = getResult.ResultData;
        user.Update(dto);

        var updateResult = await _userService.Update(user);

        if (!updateResult.Success) throw updateResult.Exception;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
        var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        if (id.ToString() != userId && userRole != "Administrator")
        {
            return Unauthorized("You are not authorized to delete this account.");
        }

        var deleteResult = await _userService.Delete(id);

        if (!deleteResult.Success) throw deleteResult.Exception;

        return NoContent();
    }

    [HttpPost("register"), AllowAnonymous]
    public async Task<ActionResult> Register([FromBody] UserRegistrationDto dto)
    {
        User user = _mapper.Map<User>(dto);

        var registerResult = await _userService.Register(user, dto.Password);

        if (!registerResult.Success) throw registerResult.Exception;

        return StatusCode(201);
    }

    [HttpPut("set-roles/{id}"), Authorize(Roles = "Administrator")]
    public async Task<ActionResult> SetRoles(Guid id, IEnumerable<string> roles)
    {
        var getResult = await _userService.Get(id);

        if (!getResult.Success && getResult.Exception is KeyNotFoundException) return NotFound();
        if (!getResult.Success) throw getResult.Exception;

        User user = getResult.ResultData;

        var setRolesResult = await _userService.SetRoles(user, roles);

        if (!setRolesResult.Success && setRolesResult.Exception is KeyNotFoundException) return NotFound($"No user with id {id} was found.");
        if (!setRolesResult.Success && setRolesResult.Exception is InvalidOperationException)
        {
            Regex rx = new Regex(@"^Role (?<role>\w+) does not exist\.$");
            Match match = rx.Match(setRolesResult.Exception.Message);

            if (match.Success)
                return NotFound($"Role {match.Groups["role"]} does not exist.");
        }
        if (!setRolesResult.Success) throw setRolesResult.Exception;

        return NoContent();
    }

    [HttpPost("change-password/{id}")]
    public async Task<ActionResult> ChangePassword(Guid id, string currentPassword, string newPassword)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
        var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        if (id.ToString() != userId && userRole != "Administrator")
        {
            return Unauthorized("You are not authorized to change another user's password.");
        }

        var getResult = await _userService.Get(id);

        if (!getResult.Success && getResult.Exception is KeyNotFoundException) return NotFound();
        if (!getResult.Success) throw getResult.Exception;

        User user = getResult.ResultData;

        var changePasswordResult = await _userService.ChangePassword(user, currentPassword, newPassword);

        if (!changePasswordResult.Success && changePasswordResult.Exception is InvalidOperationException) return Forbid();
        if (!changePasswordResult.Success) throw changePasswordResult.Exception;

        return NoContent();
    }
}