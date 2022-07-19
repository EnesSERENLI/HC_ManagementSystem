using HC.Application.Extensions;
using HC.Presentation.Areas.Waiter.Models;
using HC.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace HC.Presentation.Areas.Waiter.Controllers
{
    [Area("Waiter")]
    public class CartController : Controller
    {
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
        public IActionResult Increase(Guid id, int tableId)
        {
            var productList = HttpContext.Session.GetProductJson<List<CartItem>>($"scart{tableId}");
            CartItem cartItem = productList.Where(x => x.ProductId == id).FirstOrDefault();
            cartItem.Quantity++;

            SessionHelper.SetProductJson(HttpContext.Session, $"scart{tableId}", productList);
            return RedirectToAction("index", new { id = tableId });
        }
        #endregion

        #region DecreaseOrder Quantity
        public IActionResult Decrease(Guid id, int tableId)
        {
            var productList = HttpContext.Session.GetProductJson<List<CartItem>>($"scart{tableId}");
            CartItem cartItem = productList.Where(x => x.ProductId == id).FirstOrDefault();
            if (cartItem.Quantity > 1)
            {
                cartItem.Quantity--;
                HttpContext.Session.SetProductJson($"scart{tableId}", productList);
            }
            else if (cartItem.Quantity == 1)
            {
                TempData["message"] = "Order quantity must be at least 1!!";
            }
            return RedirectToAction("index", new { id = tableId });
        }
        #endregion

        public IActionResult DeleteOrder(Guid id, int tableId)
        {
            var productList = HttpContext.Session.GetProductJson<List<CartItem>>($"scart{tableId}");
            CartItem cartItem = productList.Where(x => x.ProductId == id).FirstOrDefault();
            productList.Remove(cartItem);
            if (productList.Count > 0)
                SessionHelper.SetProductJson(HttpContext.Session, $"scart{tableId}", productList);
            else
                HttpContext.Session.Remove($"scart{tableId}");

            return RedirectToAction("index", new { id = tableId });
        }

    }
}
