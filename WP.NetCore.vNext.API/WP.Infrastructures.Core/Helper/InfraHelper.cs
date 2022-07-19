using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Infrastructures.Core.Helper;

namespace WP.Infrastructures.Core
{
    public class InfraHelper
    {
        public static ISecurity Security => new Security();

        public static IHashGenerater Hash => new HashGenerater();
    }
}
