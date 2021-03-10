using System;
using System.Collections.Generic;

namespace Extensions
{
    public static class Extensions
    {
        public static int IToInt(this object x)
        {
            int rtnValue;
            var str = Convert.ToString(x);
            if (!int.TryParse(str,out rtnValue))
            {
                throw new Exception($"Could not convert {str} to an interger.");
            }
            return rtnValue;
        }
    }
}
