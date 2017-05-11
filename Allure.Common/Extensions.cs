using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Common
{
    public static class Extensions
    {
        public static bool In<T>(this T value, params T[] values)
        {
            return values.Contains(value);
        }

        public static bool In<T>(this T value, IEqualityComparer<T> comparer, params T[] values)
        {
            return values.Contains(value, comparer);
        }
    }
}
