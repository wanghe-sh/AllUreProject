using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Common
{
    public abstract class A<Temp> where Temp : class
    {
        public static TEnum[] GetValues<TEnum>() where TEnum : struct, Temp
        {
            return (TEnum[])Enum.GetValues(typeof(TEnum));
        }
    }

    public class Enums : A<Enum>
    {
    }
}
