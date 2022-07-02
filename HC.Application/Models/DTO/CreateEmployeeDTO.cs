using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Models.DTO
{
    public class CreateEmployeeDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public int DepartmentId { get; set; }
    }
}
