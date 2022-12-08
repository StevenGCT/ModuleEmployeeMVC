using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ModuleEmployeeMVC.Context;
using ModuleEmployeeMVC.Models;
using Newtonsoft.Json;

namespace ModuleEmployeeMVC.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly AplicationDBContext _context;
        //CONSUMING SERVICES
        static HttpClient client = new HttpClient();

        public EmployeesController(AplicationDBContext context)
        {
            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri("https://localhost:7164/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }

            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            List<Employee> list = null;

            HttpResponseMessage response = await client.GetAsync("api/Employees");
            if (response.IsSuccessStatusCode)
            {
                var resultString = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<Employee>>(resultString);
                client.DefaultRequestHeaders.Clear();
                return View(list.ToList());
            }

            return View(list.ToList());
        }

        public async Task<IActionResult> GetImage(string path)
        {
            HttpResponseMessage response = await client.GetAsync("api/StaticFiles/img" + path);
            return View(response);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }
            Employee e = null;

            HttpResponseMessage response = await client.GetAsync($"api/Employees/{id.ToString()}");
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var resultString = response.Content.ReadAsStringAsync().Result;
                e = JsonConvert.DeserializeObject<Employee>(resultString);
                return View(e);
            }
            return View(e);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,Name,LastName,SecondLastName,Phone,Address,Type,Ci,Birthday,Photo,Status,RegisterDate")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/Employees", employee);
                //HttpResponseMessage responseImage = await client.PostAsJsonAsync("api/Employees", employee);
                response.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "Employees");
            }
            return View(employee);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/UploadImage", file);
                //HttpResponseMessage responseImage = await client.PostAsJsonAsync("api/Employees", employee);
                response.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "Employees");
            }
            return View(file);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Employee e = null;

            HttpResponseMessage response = await client.GetAsync($"api/Employees/{id.ToString()}");
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var resultString = response.Content.ReadAsStringAsync().Result;
                e = JsonConvert.DeserializeObject<Employee>(resultString);
                return View(e);
            }
            return View(e);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,Name,LastName,SecondLastName,Phone,Address,Type,Ci,Birthday,Photo,Status,RegisterDate")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    HttpResponseMessage responde = await client.PutAsJsonAsync($"api/Employees/{id.ToString()}", employee);
                    responde.EnsureSuccessStatusCode();
                    return RedirectToAction("Index", "Employees");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Employee e = null;

            HttpResponseMessage response = await client.GetAsync($"api/Employees/{id.ToString()}");
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var resultString = response.Content.ReadAsStringAsync().Result;
                e = JsonConvert.DeserializeObject<Employee>(resultString);
                return View(e);
            }
            return View(e);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set 'AplicationDBContext.Employees'  is null.");
            }

            HttpResponseMessage responde = await client.DeleteAsync($"api/Employees/{id.ToString()}");
            responde.EnsureSuccessStatusCode();
            return RedirectToAction("Index", "Employees");
        }

        private bool EmployeeExists(int id)
        {
          return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
