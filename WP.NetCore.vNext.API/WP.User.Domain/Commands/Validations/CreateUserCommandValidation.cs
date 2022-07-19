namespace WP.User.Domain.Commands.Validations;

public class CreateUserCommandValidation : UserValidation<CreateUserCommand>
{
    public CreateUserCommandValidation()
    {
        ValidateAccount();
        ValidatePassword();
    }
}


