

namespace WP.User.Application.Contracts.Dtos.Role;

public class RoleDto: AuditDto
{
    public string Name { get; set; }

    public int Sort { get; set; }

    public long? PId { get; set; }

    public string Desc { get; set; }
}
