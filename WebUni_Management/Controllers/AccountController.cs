using Microsoft.AspNetCore.Mvc;

namespace WebUni_Management.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
