using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Models.DTO
{
    public class UpdateEmployeeDTO
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string CreatedComputerName { get; set; }
        public string CreatedIP { get; set; }
        public string CreatorUserName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedComputerName { get; set; }
        public string UpdatedIP { get; set; }
        public string UpdaterUserName { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string DeletedComputerName { get; set; }
        public string DeletedIP { get; set; }
        public string DeleterUserName { get; set; }
    }
}
