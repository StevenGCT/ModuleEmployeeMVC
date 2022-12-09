using Microsoft.AspNetCore.Mvc;
using ModuleEmployeeMVC.Context;
using ModuleEmployeeMVC.Models;
using System.Net.Http.Headers;

namespace ModuleEmployeeMVC.Controllers
{
    public class EmployeeEventController : Controller
    {
        private readonly AplicationDBContext _context;

        static HttpClient client = new HttpClient();

        public EmployeeEventController(AplicationDBContext context)
        {
            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri("https://localhost:7164/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }

            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId, EventId")] EmployeeEvent @employeeEvent)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/AddEmployeeToEvent", @employeeEvent);
                response.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "EmployeeEvent");
            }
            return View(@employeeEvent);
        }
    }
}
