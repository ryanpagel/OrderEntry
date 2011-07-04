using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickBooks.Util
{
    public class Util
    {

        public T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }
    }
}
