using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.User.Domain.Commands.Validations;

namespace WP.User.Domain.Commands
{
    public class UserLoginCommand : UserCommand
    {
        public UserLoginCommand(string account,string password)
        {
            this.Account = account;
            this.Password = password;
        }


        public override bool IsValid()
        {
            ValidationResult = new UserLoginCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
