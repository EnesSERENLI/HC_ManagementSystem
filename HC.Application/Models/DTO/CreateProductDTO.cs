using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Models.DTO
{
    public class CreateProductDTO
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }
        public string ImagePath { get; set; }
        public int SubCategoryId { get; set; }

    }
}
