using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.User.Domain.Commands
{
    public class RemoveUserCommand : UserCommand
    {
        public RemoveUserCommand(long id)
        {
            this.Id = id;
            AggregateId = id;
        }


        public override bool IsValid()
        {
            ValidationResult = new RemoveUserCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
