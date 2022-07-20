using HC.Application.Models.VM;
using HC.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Service.Interface
{
    public interface IOrderService
    {
        public Task<string> Create(Order model);
        public Task<List<OrderVM>> GetOrders();
        public Task<OrderVM> GetById(Guid id);
    }
}
