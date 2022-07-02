using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Models.DTO
{
    public class UpdateSubCategoryDTO
    {
        public Guid ID { get; set; }
        public string SubCategoryName { get; set; }
        public string Description { get; set; }
    }
}
