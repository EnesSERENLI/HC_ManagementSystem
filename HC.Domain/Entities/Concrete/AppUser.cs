using HC.Domain.Entities.Interface;
using HC.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Domain.Entities.Concrete
{
    public class AppUser : IdentityUser, IBaseEntity
    {
        public AppUser()
        {
            Status = Status.Active;
        }
        public Guid ID { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string ImagePath { get; set; }

        public Status Status { get; set; }
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

        //Relations
        public virtual List<Order> Orders { get; set; }
    }
}
