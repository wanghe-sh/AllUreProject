using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Models
{
    public class StorageOperationDetail
    {
        public int Id { get; set; }

        public int StorageOperationId { get; set; }

        public int ProductId { get; set; }

        public decimal CurrentBalance { get; set; }

        public decimal OperationCount { get; set; }

        public virtual Product Product { get; set; }
    }
}
