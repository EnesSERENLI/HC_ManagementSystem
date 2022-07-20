using AutoMapper;
using HC.Application.Models.VM;
using HC.Application.Service.Interface;
using HC.Domain.Entities.Concrete;
using HC.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Service.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        

        public async Task<string> Create(Order model)
        {
            try
            {
                //var order = _mapper.Map<Order>(model);

                await _unitOfWork.OrderRepository.Add(model);

                return "Order complated!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public Task<OrderVM> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OrderVM>> GetOrders()
        {
            var orderList = await _unitOfWork.OrderDetailRepository.GetFilteredFirstOrDefaults(selector: x => new OrderVM
            {
                OrderID = x.OrderId,
                EmployeeName = x.Order.Employee.FirstName + " " + x.Order.Employee.LastName,
                ProductName = x.Product.ProductName,
                UserName = x.Order.AppUser.UserName,
                UnitPrice = x.UnitPrice,
                Quantity = x.Quantity,
            },
            expression: x => x.Status != Domain.Enums.Status.None
            );

            return orderList;
        }
    }
}
