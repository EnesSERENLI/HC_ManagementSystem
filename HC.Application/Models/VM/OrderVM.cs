using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Models.VM
{
    public class OrderVM
    {
        public Guid ProductId { get; set; }
        public Guid EmployeeId { get; set; }
        public string UserId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? SubTotal { get => UnitPrice * Quantity; }
    }
}
