using GameDevPortal.Core.Entities;
using GameDevPortal.Core.Interfaces.Authentication;
using GameDevPortal.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GameDevPortal.WebAPI.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly ILogger _logger;

    public UserService(UserManager<User> userManager, ILogger logger)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<OperationResult<IEnumerable<User>>> List(Pagination pagination, CancellationToken cancellationToken = default)
    {
        try
        {
            IEnumerable<User> users = await _userManager.Users.OrderBy(u => u.Id).Skip(pagination.Size * pagination.Index).Take(pagination.Size).ToListAsync(cancellationToken);

            return OperationResult<IEnumerable<User>>.CreateSuccessResult(users);
        }
        catch (Exception ex)
        {
            return OperationResult<IEnumerable<User>>.CreateFailure(ex);
        }
    }

    public async Task<OperationResult> Create(User user, string password, IEnumerable<string> roles)
    {
        try
        {
            var createResult = await _userManager.CreateAsync(user, password);

            if (!createResult.Succeeded)
            {
                string aggregatedError = createResult.Errors.Select(e => $"{e.Code} {e.Description}").Aggregate((s1, s2) => $"{s1} {s2}");
                _logger.LogError(aggregatedError);
                return OperationResult.CreateFailure(new Exception(aggregatedError));
            }

            var addRolesResult = await _userManager.AddToRolesAsync(user, roles);
            if (!addRolesResult.Succeeded)
            {
                string aggregatedError = addRolesResult.Errors.Select(e => $"{e.Code} {e.Description}").Aggregate((s1, s2) => $"{s1} {s2}");
                _logger.LogError(aggregatedError);
                return OperationResult.CreateFailure(new Exception(aggregatedError));
            }
            return OperationResult.CreateSuccessResult();
        }
        catch (Exception ex)
        {
            return OperationResult.CreateFailure(ex);
        }
    }

    public async Task<OperationResult<User>> Get(Guid id)
    {
        try
        {
            User? user = await _userManager.FindByIdAsync(id.ToString());

            if (user is null)
            {
                return OperationResult<User>.CreateFailure(new KeyNotFoundException($"No user with id {id} was found."));
            }

            return OperationResult<User>.CreateSuccessResult(user);
        }
        catch (Exception ex)
        {
            return OperationResult<User>.CreateFailure(ex);
        }
    }

    public async Task<OperationResult> Update(User user)
    {
        try
        {
            await _userManager.UpdateAsync(user);
            return OperationResult.CreateSuccessResult();
        }
        catch (Exception ex)
        {
            return OperationResult.CreateFailure(ex);
        }
    }

    public async Task<OperationResult> Delete(Guid id)
    {
        try
        {
            User user = await _userManager.FindByIdAsync(id.ToString()) ?? throw new KeyNotFoundException($"No user with Id: {id} was found.");
            await _userManager.DeleteAsync(user);
            return OperationResult.CreateSuccessResult();
        }
        catch (Exception ex)
        {
            return OperationResult<IEnumerable<User>>.CreateFailure(ex);
        }
    }

    public async Task<OperationResult<bool>> ValidatePassword(User user, string password)
    {
        try
        {
            bool passwordCorrect = await _userManager.CheckPasswordAsync(user, password);

            return OperationResult<bool>.CreateSuccessResult(passwordCorrect);
        }
        catch (Exception ex)
        {
            return OperationResult<bool>.CreateFailure(ex);
        }
    }

    public async Task<OperationResult<IList<string>>> GetRoles(User user)
    {
        try
        {
            IList<string> roles = await _userManager.GetRolesAsync(user);

            return OperationResult<IList<string>>.CreateSuccessResult(roles);
        }
        catch (Exception ex)
        {
            return OperationResult<IList<string>>.CreateFailure(ex);
        }
    }

    public async Task<OperationResult<User>> Find(string username)
    {
        try
        {
            User? user = await _userManager.FindByNameAsync(username);

            if (user is null)
            {
                return OperationResult<User>.CreateFailure(new KeyNotFoundException());
            }

            return OperationResult<User>.CreateSuccessResult(user);
        }
        catch (Exception ex)
        {
            return OperationResult<User>.CreateFailure(ex);
        }
    }

    public async Task<OperationResult> Register(User user, string password)
    {
        try
        {
            var createResult = await _userManager.CreateAsync(user, password);
            if (!createResult.Succeeded)
            {
                string aggregatedError = createResult.Errors.Select(e => $"{e.Code} {e.Description}").Aggregate((s1, s2) => $"{s1} {s2}");
                _logger.LogError(aggregatedError);
                return OperationResult.CreateFailure(new Exception(aggregatedError));
            }

            var addRolesResult = await _userManager.AddToRolesAsync(user, new[] { "User" });
            if (!addRolesResult.Succeeded)
            {
                string aggregatedError = addRolesResult.Errors.Select(e => $"{e.Code} {e.Description}").Aggregate((s1, s2) => $"{s1} {s2}");
                _logger.LogError(aggregatedError);
                return OperationResult.CreateFailure(new Exception(aggregatedError));
            }

            return OperationResult.CreateSuccessResult();
        }
        catch (Exception ex)
        {
            return OperationResult.CreateFailure(ex);
        }
    }

    public async Task<OperationResult> SetRoles(User user, IEnumerable<string> roles)
    {
        try
        {
            var currentRoles = await _userManager.GetRolesAsync(user);

            var rolesToRemove = currentRoles.Except(roles);
            if (rolesToRemove.Any())
            {
                await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
            }

            var newRoles = roles.Except(currentRoles);
            if (newRoles.Any())
            {
                try
                {
                    await _userManager.AddToRolesAsync(user, newRoles);
                }
                catch (Exception ex)
                {
                    return OperationResult.CreateFailure(ex);
                }
            }

            return OperationResult.CreateSuccessResult();
        }
        catch (Exception ex)
        {
            return OperationResult.CreateFailure(ex);
        }
    }

    public async Task<OperationResult> ChangePassword(User user, string currentPassword, string newPassword)
    {
        try
        {
            var authenticateResult = await ValidatePassword(user, currentPassword);

            if (!authenticateResult.Success) throw authenticateResult.Exception;

            bool passwordCorrect = authenticateResult.ResultData;
            if (!passwordCorrect) return OperationResult.CreateFailure(new InvalidOperationException("Current password incorrect."));

            await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            return OperationResult.CreateSuccessResult();
        }
        catch (Exception ex)
        {
            return OperationResult<bool>.CreateFailure(ex);
        }
    }
}