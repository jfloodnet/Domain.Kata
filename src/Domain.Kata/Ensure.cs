using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Kata
{
    public static class Ensure
    {
        public static void NotNull(object obj, string param)
        {
            if (obj == null)
                throw new ArgumentNullException(param);
        }
    }
}
