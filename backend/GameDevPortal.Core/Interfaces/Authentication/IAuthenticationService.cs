
using GameDevPortal.Core.Entities;
using GameDevPortal.Core.Models;

namespace GameDevPortal.Core.Interfaces.Authentication;

public interface IAuthenticationService
{
    Task<OperationResult<User>> ValidateUser(string username, string password); 
    Task<OperationResult<TokenDto>> CreateToken(User user);
    string CreateExpiredToken();
} 