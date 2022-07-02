using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Models.VM
{
    public class EmployeeVM
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
    }
}
