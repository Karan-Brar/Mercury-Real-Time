using Microsoft.AspNetCore.Mvc;

namespace MercuryMVC.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult LoginForm()
        {
            return View();
        }

        public IActionResult RegisterForm()
        {
            return View();
        }
    }
}
