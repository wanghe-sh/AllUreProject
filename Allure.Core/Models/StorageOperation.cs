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

        public string PurchaseOrderNumber { get; set; }

        public int WarehouseId { get; set; }

        public int? LogisticId { get; set; }

        public string LogisticNumber { get; set; }

        public decimal LogisticExpense { get; set; }

        public string Comment { get; set; }

        public int CreatorId { get; set; }

        public DateTime CreateTime { get; set; }

        public int UpdaterId { get; set; }

        public DateTime UpdateTime { get; set; }

        public virtual ICollection<StorageOperationDetail> Details { get; set; }

        public virtual Warehouse Warehouse { get; set; }

        public virtual Logistic Logistic { get; set; }

        public virtual Order Order { get; set; }

        public virtual User CreateBy { get; set; }

        public virtual User UpdateBy { get; set; }
    }
}
