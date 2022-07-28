namespace WP.User.Application.Dtos;

public class UserLoginDto
{
    /// <summary>
    /// 账户
    /// </summary>
    public string Account { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; }
}
