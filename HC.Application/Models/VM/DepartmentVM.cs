using HC.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Models.VM
{
    public class DepartmentVM
    {
        public Guid ID { get; set; }
        public string DepartmentName { get; set; }

        public Status Status { get; set; }
    }
}
