using GameDevPortal.Core.Entities;
using GameDevPortal.Core.Models;
using System.Runtime.CompilerServices;

namespace GameDevPortal.Core.Interfaces.Authentication;

public interface IUserService
{
    Task<OperationResult<IEnumerable<User>>> List(Pagination pagination, CancellationToken cancellationToken = default);
    Task<OperationResult> Create(User user, string password, IEnumerable<string> roles);
    Task<OperationResult<User>> Get(Guid id);
    Task<OperationResult> Update(User user);
    Task<OperationResult> Delete(Guid id);

    Task<OperationResult<bool>> ValidatePassword(User user, string password);
    Task<OperationResult<IList<string>>> GetRoles(User user);
    Task<OperationResult<User>> Find(string username);
    Task<OperationResult> Register(User user, string password);
    Task<OperationResult> SetRoles(User user, IEnumerable<string> roles);
    Task<OperationResult> ChangePassword(User user, string currentPassword, string newPassword);
}