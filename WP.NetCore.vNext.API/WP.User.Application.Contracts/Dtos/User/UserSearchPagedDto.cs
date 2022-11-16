
namespace WP.User.Application.Contracts.Dtos.User;

public class UserSearchPagedDto: QueryPagedDto
{
    /// <summary>
    /// 用户姓名
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 用户账户
    /// </summary>
    public string? Account { get; set; }
}
