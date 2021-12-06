using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.Extensions
{
    public static class EnumExtensions
    {
        public static int IntValue(this Enum value)
        {
            return Convert.ToInt32(value);
        }
    }
}
