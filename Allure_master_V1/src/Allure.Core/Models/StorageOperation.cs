using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Models
{
    public class StorageOperation
    {
        public StorageOperation()
        {
            this.Details = new List<StorageOperationDetail>();
        }

        public int Id { get; set; }

        public StorageOperationType Type { get; set; }

        public int? OrderId { get; set; }

        public string WarehouseCode { get; set; }

        public string LogisticCode { get; set; }

        public string LogisticOrderNumber { get; set; }

        public decimal LogisticFee { get; set; }

        public string Comment { get; set; }

        public DateTime CreateDate { get; set; }

        public int CreateBy { get; set; }

        public DateTime UpdateDate { get; set; }

        public int UpdateBy { get; set; }

        public virtual ICollection<StorageOperationDetail> Details { get; set; }

        public virtual Order Order { get; set; }

        public virtual Warehouse Warehouse { get; set; }

        public virtual Logistic Logistic { get; set; }
    }
}
