using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Models
{
    public class LocalizedSysText : ILocalized
    {
        public string SysTextCode { get; set; }

        public string LanguageCode { get; set; }

        public string Text { get; set; }

        public virtual SysText SysText { get; set; }

        public virtual Language Language { get; set; }
    }
}
