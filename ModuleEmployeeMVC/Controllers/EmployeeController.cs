using Microsoft.AspNetCore.Mvc;

namespace ModuleEmployeeMVC.Controllers
{
    public class EmployeeController : Controller
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
