using HC.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Models.VM
{
    public class SubCategoryVM
    {
        public Guid? ID { get; set; }
        public string SubCategoryName { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public Status Status { get; set; }
    }
}
