
namespace WP.User.Domain.Commands;

public class CreateUserCommand : UserCommand
{
    public CreateUserCommand(string account, string password,string name,int sex)
    {
        this.Account = account;
        this.Password = password;
        this.Name = name;
        this.Sex = sex;
    }


    public override bool IsValid()
    {
        ValidationResult = new CreateUserCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
