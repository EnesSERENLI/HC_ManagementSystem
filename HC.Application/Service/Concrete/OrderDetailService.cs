using AutoMapper;
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
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderDetailService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<string> Create(OrderDetail model)
        {
            try
            {
                //var order = _mapper.Map<Order>(model);

                await _unitOfWork.OrderDetailRepository.Add(model);

                return "Order complated!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
