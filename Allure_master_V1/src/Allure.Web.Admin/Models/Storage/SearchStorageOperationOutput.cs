using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Admin.Models
{
    public class SearchStorageOperationOutput
    {
        public int Count { get; set; }

        public StorageOperationListOutput[] StorageOperations { get; set; }
    }
}