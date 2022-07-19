
namespace WP.User.Domain.Commands;

public class CreateUserCommand : UserCommand
{
    public CreateUserCommand(string account, string password)
    {
        this.Account = account;
        this.Password = password;
    }


    public override bool IsValid()
    {
        ValidationResult = new CreateUserCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
