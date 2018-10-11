using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Admin.Models
{
    public class SearchUserOutput
    {
        public int Count { get; set; }

        public UserOutput[] Users { get; set; }
    }
}