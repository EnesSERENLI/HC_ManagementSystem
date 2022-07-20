using HC.Application.Extensions;
using HC.Application.Service.Interface;
using HC.Domain.Entities.Concrete;
using HC.Presentation.Areas.Waiter.Models;
using HC.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace HC.Presentation.Areas.Waiter.Controllers
{
    [Area("Waiter")]
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;

        public CartController(IProductService productService,IOrderService orderService,IOrderDetailService orderDetailService)
        {
            _productService = productService;
            _orderService = orderService;
            _orderDetailService = orderDetailService;
        }
        public static List<Table> tableList = new List<Table>
        {
            new Table{Id = 1,TableName ="DinnerTable 1",Status = Domain.Enums.Status.None},
            new Table{Id = 2,TableName ="DinnerTable 2",Status = Domain.Enums.Status.None},
            new Table{Id = 3,TableName ="DinnerTable 3",Status = Domain.Enums.Status.None},
            new Table{Id = 4,TableName ="DinnerTable 4",Status = Domain.Enums.Status.None},
            new Table{Id = 5,TableName ="DinnerTable 5",Status = Domain.Enums.Status.None}
        };

        public IActionResult Index(int id)
        {
            var cart = SessionHelper.GetProductJson<List<CartItem>>(HttpContext.Session, $"scart{id}");
            if (cart != null)
            {
                return View(cart);
            }
            TempData["message"] = "An order for this table has not been opened!";
            return View();
        }

        #region IncreaseOrder Quantity
        public async Task<IActionResult> Increase(Guid id, int tableId)
        {
            var productList = HttpContext.Session.GetProductJson<List<CartItem>>($"scart{tableId}");
            CartItem cartItem = productList.Where(x => x.ProductId == id).FirstOrDefault();
            cartItem.Quantity++;

            SessionHelper.SetProductJson(HttpContext.Session, $"scart{tableId}", productList);

            var product = await _productService.GetById(cartItem.ProductId);
            product.UnitsInStock--;
            await _productService.Update(product);

            return RedirectToAction("index", new { id = tableId });
        }
        #endregion

        #region DecreaseOrder Quantity
        public async Task<IActionResult> Decrease(Guid id, int tableId)
        {
            var productList = HttpContext.Session.GetProductJson<List<CartItem>>($"scart{tableId}");
            CartItem cartItem = productList.Where(x => x.ProductId == id).FirstOrDefault();
            if (cartItem.Quantity > 1)
            {
                cartItem.Quantity--;
                HttpContext.Session.SetProductJson($"scart{tableId}", productList);

                var product = await _productService.GetById(cartItem.ProductId);
                product.UnitsInStock++;
                await _productService.Update(product);
            }
            else
            {
                TempData["message"] = "Order quantity must be at least 1!!";
            }
            return RedirectToAction("index", new { id = tableId });
        }
        #endregion

        #region DeleteOrder
        public async Task<IActionResult> DeleteOrder(Guid id, int tableId)
        {
            var productList = HttpContext.Session.GetProductJson<List<CartItem>>($"scart{tableId}");
            CartItem cartItem = productList.Where(x => x.ProductId == id).FirstOrDefault();

            var product = await _productService.GetById(id);
            product.UnitsInStock += cartItem.Quantity;
            await _productService.Update(product);

            productList.Remove(cartItem);
            if (productList.Count > 0)
                SessionHelper.SetProductJson(HttpContext.Session, $"scart{tableId}", productList);
            else
                HttpContext.Session.Remove($"scart{tableId}");

            return RedirectToAction("index", new { id = tableId });
        }
        #endregion

        public async Task<IActionResult> ComplateOrder(int id)
        {
            var products = HttpContext.Session.GetProductJson<List<CartItem>>($"scart{id}");
            CartItem ci = products.Where(x => x.TableId == id).FirstOrDefault();
            Order order = new Order();            
            order.EmployeeId = ci.EmployeeId;
            order.AppUserId = ci.AppUserId;
            await _orderService.Create(order);
            foreach (var cartItem in products)
            {
                var product = await _productService.GetById(cartItem.ProductId);
                if (product.UnitsInStock <= 50)
                {
                    //todo: Burada stok 50 den aşağı inmiş ise satın alma departmanına uyarı maili atılacak!                
                }
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.ProductId = cartItem.ProductId;
                orderDetail.Quantity = cartItem.Quantity;
                orderDetail.UnitPrice = cartItem.Price;
                orderDetail.OrderId = order.ID;
                await _orderDetailService.Create(orderDetail);
            }
            
            HttpContext.Session.Remove($"scart{id}");
            return RedirectToAction("Index", new { id = id });
        }

    }
}
