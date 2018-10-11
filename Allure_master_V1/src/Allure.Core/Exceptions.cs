using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core
{
    public class InvalidUserNameException : Exception
    {
        public InvalidUserNameException(string userName)
            : base(string.Format("user '{0}' doesn't exist", userName))
        { }
    }

    public class WrongPasswordException : Exception
    {
        public WrongPasswordException()
            : base("incorrect password")
        { }
    }
}
