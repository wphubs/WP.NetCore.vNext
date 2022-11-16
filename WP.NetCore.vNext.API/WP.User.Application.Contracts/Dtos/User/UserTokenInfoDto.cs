namespace WP.User.Application.Contracts.Dtos.User;

public class UserTokenInfoDto
{
    /// <summary>
    /// 访问Token
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    /// 刷新Token
    /// </summary>
    public string RefreshToken { get; set; }
}
