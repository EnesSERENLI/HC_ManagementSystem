using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Models.DTO
{
    public class UpdateProductDTO
    {
        public Guid ID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }
        public string ImagePath { get; set; }
        public IFormFile Image { get; set; }
        public Guid SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }

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
