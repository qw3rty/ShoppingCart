using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart.Core.Components.Helper
{
    public static class StringHelper
    {
        public static string ToNewLineString(this List<string> stringList)
        {
            if (stringList == null)
                return String.Empty;
            
            return stringList.Aggregate((i, j) => i + Environment.NewLine + j);
        }
    }
}