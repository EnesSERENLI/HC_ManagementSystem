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
            var cart = SessionHelper.GetProductJson<List<CartItem>>(HttpContext.Session,$"scart{id}");
            if (cart != null)
            {
                return View(cart);
            }
            TempData["message"] = "An order for this table has not been opened!";
            return View();
        }
    }
}
