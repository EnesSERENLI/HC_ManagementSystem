using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Models.DTO
{
    public class UpdateCategoryDTO
    {
        public Guid ID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}
