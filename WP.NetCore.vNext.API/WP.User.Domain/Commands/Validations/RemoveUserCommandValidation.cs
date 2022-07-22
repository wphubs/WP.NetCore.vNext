using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.User.Domain.Commands.Validations;

public class RemoveUserCommandValidation : UserValidation<RemoveUserCommand>
{
    public RemoveUserCommandValidation()
    {
        ValidateId();
    }
}