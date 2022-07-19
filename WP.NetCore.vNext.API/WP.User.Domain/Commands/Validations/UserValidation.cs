using FluentValidation;
namespace WP.User.Domain.Commands.Validations;

public class UserValidation<T> : AbstractValidator<T> where T : UserCommand
{
    protected void ValidateAccount()
    {
        RuleFor(c => c.Account)
            .NotEmpty().WithMessage("用户名不能为空");
    }

    protected void ValidatePassword()
    {
        RuleFor(c => c.Password)
            .NotEmpty().WithMessage("密码不能为空");
    }

}
