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
        public async Task<IActionResult> Index(string? type)
        {
            List<Employee> list = null;

            if (!String.IsNullOrEmpty(type))
            {
                HttpResponseMessage response2 = await client.GetAsync("api/TypeEmployee/" + type);
                if (response2.IsSuccessStatusCode)
                {
                    var resultString = response2.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<Employee>>(resultString);
                    client.DefaultRequestHeaders.Clear();
                    return View(list.ToList());
                }
            }
            else
            {
                HttpResponseMessage response = await client.GetAsync("api/Employees");
                if (response.IsSuccessStatusCode)
                {
                    var resultString = response.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<Employee>>(resultString);
                    client.DefaultRequestHeaders.Clear();
                    return View(list.ToList());
                }
            }
            return View(list.ToList());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
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
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
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
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
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
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
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
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
          return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
