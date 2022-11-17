
namespace WP.User.Application.Contracts.Dtos.User;

public abstract class UserCreateAndUpdateDto
{
    public string Name { get; set; }

    public string Account { get; set; }


    public int? Sex { get; set; }

    public List<long> Roles { get; set; }

}
