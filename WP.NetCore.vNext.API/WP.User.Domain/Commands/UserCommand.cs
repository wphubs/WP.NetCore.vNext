
namespace WP.User.Domain.Commands;

public abstract class UserCommand : Command
{
    public long Id { get; protected set; }

    public string Account { get; protected set; }
    public string Avatar { get; protected set; }

    public long? DeptId { get; protected set; }

    public string Name { get; protected set; }

    public string Password { get; protected set; }

    //public string Salt { get; protected set; }

    public int? Sex { get; protected set; }
}
