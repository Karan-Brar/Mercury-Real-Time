using Microsoft.AspNetCore.Mvc;

namespace MercuryMVC.Controllers
{
    public class ChatRoomController : Controller
    {
        public IActionResult ChatRoom()
        {
            return View();
        }
    }
}
