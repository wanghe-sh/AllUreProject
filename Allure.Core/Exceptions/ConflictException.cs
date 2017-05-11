using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException(string message)
            : base(message)
        { }
    }

    public class ConflictException<T> : ConflictException
    {
        public ConflictException(string propertyName, object value)
            : base($"{typeof(T).Name}.{propertyName}={value} conflicts with existing records")
        { }
    }
}
