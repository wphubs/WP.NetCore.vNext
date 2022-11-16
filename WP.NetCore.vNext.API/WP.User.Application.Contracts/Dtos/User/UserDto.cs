
public class UserDto: AuditDto
{

    public string Account { get; set; }

  
    public string Avatar { get; set; }


    public string Name { get; set; }


    public int? Sex { get; set; }

    public string SexName
    {
        get
        {
            string result = "未知";
            if (this.Sex.HasValue)
            {
                result = this.Sex.Value == 1 ? "男" : "女";
            }

            return result;
        }
    }
    /// <summary>
    /// 账户状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 账户状态描述
    /// </summary>
    public string StatusName => this.Status == 1 ? "启用" : "禁用";


    public List<RoleDto> Roles { get; set; }
}


