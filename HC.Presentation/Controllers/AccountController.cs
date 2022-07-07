using Microsoft.AspNetCore.Mvc;

namespace HC.Presentation.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
