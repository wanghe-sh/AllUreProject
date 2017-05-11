using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Common
{
    public class CustomEqualityComparer
    {
        public static IEqualityComparer<T> Create<T>(params Func<T, object>[] properties)
        {
            return new CustomComparer<T>(properties);
        }

        class CustomComparer<T> : IEqualityComparer<T>
        {
            private Func<T, object>[] _properties;
            private static int[] _primes = new[] { 3, 5, 7, 11, 13, 17, 19, 23 };

            public CustomComparer(params Func<T, object>[] properties)
            {
                _properties = properties;
            }

            bool IEqualityComparer<T>.Equals(T x, T y)
            {
                if (x == null && y == null)
                {
                    return true;
                }

                if (x == null || y == null)
                {
                    return false;
                }

                foreach (var property in _properties)
                {
                    if (!property(x).Equals(property(y)))
                    {
                        return false;
                    }
                }

                return true;
            }

            int IEqualityComparer<T>.GetHashCode(T obj)
            {
                var hashCode = 0;

                for (int i = 0; i < _properties.Length; i++)
                {
                    hashCode += _primes[i % _primes.Length] * _properties[i](obj).GetHashCode();
                }

                return hashCode;
            }
        }
    }
}
