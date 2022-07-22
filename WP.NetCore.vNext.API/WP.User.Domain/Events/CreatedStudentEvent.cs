using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.User.Domain.Events
{
    public class CreatedStudentEvent : Event
    {
        public CreatedStudentEvent(long id, string account, string name)
        {
            this.Id = id;
            this.Account = account;
            this.Name = name;
        }

        public long Id { get;  }

        public string Account { get;  }
        public string Avatar { get;  }

        public long? DeptId { get;  }

        public string Name { get; }

        public string Password { get; }

        public int? Sex { get;}

    }
}
