﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.Infrastructures.Core.Context
{
    public interface IUserContext
    {
        long Id { get; set; }

        string Name { get; set; }
    }
}
