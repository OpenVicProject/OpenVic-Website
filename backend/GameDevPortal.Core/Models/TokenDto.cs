using GameDevPortal.Core.Extensions;

namespace GameDevPortal.Core.Models;

public class TokenDto
{
    public string AccessToken { get; set; }

    public TokenDto(string accessToken)
    {
        accessToken.ThrowIfEmptyOrNull(nameof(accessToken));
        AccessToken = accessToken;
    }
}