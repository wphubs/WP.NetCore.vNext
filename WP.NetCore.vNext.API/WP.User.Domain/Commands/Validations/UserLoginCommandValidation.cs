
namespace WP.User.Domain.Commands.Validations;

public class UserLoginCommandValidation : UserValidation<UserLoginCommand>
{
    public UserLoginCommandValidation()
    {
        ValidateAccount();
        ValidatePassword();
    }
}
