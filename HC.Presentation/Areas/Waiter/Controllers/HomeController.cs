using HC.Application.Extensions;
using HC.Application.Service.Interface;
using HC.Presentation.Areas.Waiter.Models;
using HC.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace HC.Presentation.Areas.Waiter.Controllers
{
    [Area("Waiter")]
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly IEmployeeService _employeeService;
        private readonly IAppUserService _appUserService;

        public HomeController(IProductService productService, IEmployeeService employeeService,IAppUserService appUserService)
        {
            _productService = productService;
            _employeeService = employeeService;
            _appUserService = appUserService;
        }
        public static List<Table> tableList = new List<Table>
        {
            new Table{Id = 1,TableName ="DinnerTable 1",Status = Domain.Enums.Status.None},
            new Table{Id = 2,TableName ="DinnerTable 2",Status = Domain.Enums.Status.None},
            new Table{Id = 3,TableName ="DinnerTable 3",Status = Domain.Enums.Status.None},
            new Table{Id = 4,TableName ="DinnerTable 4",Status = Domain.Enums.Status.None},
            new Table{Id = 5,TableName ="DinnerTable 5",Status = Domain.Enums.Status.None}
        };

        public IActionResult Index(int? id)
        {
            if (id != null)
            {
                var table = tableList.Find(x => x.Id == id);
                if (table != null)
                {
                    table.Status = Domain.Enums.Status.None;
                }
            }
            return View(tableList);
        }

        public async Task<IActionResult> CreateOrder(int id) //masalara sipariş açarken aynı anda birden çok garson aynı masaya sipariş açmamalı! Bu yüzden masa id si gönderiyorum. Bu masaya ait sipariş ekranına girildiğinde masanın status'ü akrif olacak ve başka birisi tarafından girilmesine izin vermeyecek!
        {
            var table = tableList.Find(x => x.Id == id);
            if (table == null)
                return RedirectToAction("index");

            if (table.Status == Domain.Enums.Status.Active)
            {
                TempData["message"] = "You cannot create an order on this table at this time!";
                return RedirectToAction("index");
            }

            table.Status = Domain.Enums.Status.Active;

            ViewBag.Products = await _productService.GetDefaultProducts();
            return View(table);
        }

        public async Task<IActionResult> AddToCart(Guid id,int tableId,string userName)
        {
            try
            {
                var product = await _productService.GetById(id);
                if (product.UnitsInStock > 0)
                {
                    product.UnitsInStock--;
                    await _productService.Update(product);
                    var user = await _appUserService.GetByUser(userName);
                    List<CartItem> cartItems = HttpContext.Session.GetProductJson<List<CartItem>>($"scart{tableId}");

                    Cart c = new Cart();
                    if (cartItems != null)
                    {
                        foreach (CartItem cartItem in cartItems)
                        {
                            c.AddItem(cartItem);
                        }
                    }

                    CartItem ci = new CartItem();
                    ci.ProductId = id;
                    ci.ProductName = product.ProductName;
                    ci.Price = product.UnitPrice;
                    ci.TableId = tableId;
                    ci.AppUserId = user.Id;
                    foreach (var employee in await _employeeService.GetDefaultEmployees())
                    {
                        if (user.FullName == employee.FirstName + " " + employee.LastName)
                        {
                            ci.EmployeeId = employee.ID;
                            break;
                        }
                    }

                    c.AddItem(ci);
                    SessionHelper.SetProductJson(HttpContext.Session, $"scart{tableId}", c.myCart);
                    Table table = tableList.Find(x => x.Id == tableId);
                    table.Status = Domain.Enums.Status.None; //masaya sipariş eklediğinde tekrar masanın durumunu None'a çek. Başka siparişlerde verilebilsin.

                    return RedirectToAction("CreateOrder", new { id = tableId });
                }
                else
                {
                    TempData["message"] = "There are not enough products in stock. You cannot create an order!";
                    return RedirectToAction("CreateOrder", new { id = tableId });
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
                return RedirectToAction("CreateOrder");
            }
        }
    }
}
