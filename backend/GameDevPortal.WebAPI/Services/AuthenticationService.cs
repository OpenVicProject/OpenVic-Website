using GameDevPortal.Core.Entities;
using GameDevPortal.Core.Interfaces.Authentication;
using GameDevPortal.Core.Models;
using GameDevPortal.WebAPI.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GameDevPortal.WebAPI.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserService _userService;
    private readonly IOptionsMonitor<JwtConfiguration> _jwtConfig;

    public AuthenticationService(IUserService userService, IOptionsMonitor<JwtConfiguration> jwtConfig)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _jwtConfig = jwtConfig ?? throw new ArgumentNullException(nameof(jwtConfig));
    }

    public async Task<OperationResult<User>> ValidateUser(string username, string password)
    {
        var findUserResult = await _userService.Find(username);

        if (!findUserResult.Success)
        {
            return OperationResult<User>.CreateFailure(new KeyNotFoundException($"No user with username: {username} was found."));
        }

        User user = findUserResult.ResultData;

        var checkPasswordResult = await _userService.ValidatePassword(user, password);

        if (!checkPasswordResult.Success) throw checkPasswordResult.Exception;

        bool passwordCorrect = checkPasswordResult.ResultData;

        if (!passwordCorrect)
        {
            return OperationResult<User>.CreateFailure(new InvalidOperationException($"Entered password for user {username} was incorrect."));
        }

        return OperationResult<User>.CreateSuccessResult(user);
    }

    public async Task<OperationResult<TokenDto>> CreateToken(User user)
    {
        var signingCredentials = GetSigningCredentials();

        var getClaimsResult = await GetClaims(user);

        if (!getClaimsResult.Success)
        {
            return OperationResult<TokenDto>.CreateFailure(getClaimsResult.Exception);
        }

        List<Claim> claims = getClaimsResult.ResultData;

        JwtSecurityToken tokenOptions = GenerateTokenOptions(signingCredentials, claims, 60);
        JwtSecurityTokenHandler tokenHandler = new();
        string accessToken = tokenHandler.WriteToken(tokenOptions);

        TokenDto token = new(accessToken);

        return OperationResult<TokenDto>.CreateSuccessResult(token);
    }

    public string CreateExpiredToken()
    {
        var signingCredentials = GetSigningCredentials();
        var tokenOptions = GenerateTokenOptions(signingCredentials, new List<Claim>(), -1);
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JwtSignKey")!);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<OperationResult<List<Claim>>> GetClaims(User user)
    {
        var claims = new List<Claim> {
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim("UserId", user.Id.ToString())
        };

        var getRolesResult = await _userService.GetRoles(user);
        if (!getRolesResult.Success)
        {
            return OperationResult<List<Claim>>.CreateFailure(getRolesResult.Exception);
        }

        var roles = getRolesResult.ResultData;
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return OperationResult<List<Claim>>.CreateSuccessResult(claims);
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims, int validMinutes)
    {
        var jwtSettings = _jwtConfig.CurrentValue;

        var tokenOptions = new JwtSecurityToken(
            issuer: jwtSettings.ValidIssuer,
            audience: jwtSettings.ValidAudience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(validMinutes),
            signingCredentials: signingCredentials);

        return tokenOptions;
    }
}