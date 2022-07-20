using HC.Application.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HC.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<IActionResult> Index() =>View(await _orderService.GetOrders());
    }
}
