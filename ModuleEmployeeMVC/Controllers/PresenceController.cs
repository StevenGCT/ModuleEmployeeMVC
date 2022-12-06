using Microsoft.AspNetCore.Mvc;

namespace ModuleEmployeeMVC.Controllers
{
    public class PresenceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
