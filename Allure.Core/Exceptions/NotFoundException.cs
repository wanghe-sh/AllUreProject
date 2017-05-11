using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(Type type, string condition)
            : base($"{type.Name} {condition} can't be found")
        { }
    }

    public class NotFoundException<T> : NotFoundException
    {
        public NotFoundException(string condition)
            : base(typeof(T), condition)
        { }
    }
}
