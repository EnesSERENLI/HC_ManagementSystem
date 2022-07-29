using HC.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Models.VM
{
    public class ProductVM
    {
        public Guid ID { get; set; } //Normally it is not correct to have ID information on VMs. If necessary, different vms should be written for each get operation and information strength should be provided. But since I've been experimenting here and learning step-by-step, I've defined it now!
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }
        public string ImagePath { get; set; }
        public string SubCategoryName { get; set; }
        public Guid SubCategoryId { get; set; }
        public Status Status { get; set; }

    }
}
