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
    public class PresenceWorksController : Controller
    {
        private readonly AplicationDBContext _context;

        static HttpClient client = new HttpClient();

        public PresenceWorksController(AplicationDBContext context)
        {
            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri("https://localhost:7164/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }

            _context = context;
        }
        // GET: PresenceWorks
        public async Task<IActionResult> Index()
        {
            List<PresenceWork> list = null;

            HttpResponseMessage response = await client.GetAsync("api/PresenceWorks");
            if (response.IsSuccessStatusCode)
            {
                var resultString = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<PresenceWork>>(resultString);
                client.DefaultRequestHeaders.Clear();
                return View(list.ToList());
            }

            return View(list.ToList());
        }

        // GET: PresenceWorks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PresenceWorks == null)
            {
                return NotFound();
            }
            PresenceWork p = null;

            HttpResponseMessage response = await client.GetAsync($"api/PresenceWorks/{id.ToString()}");
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var resultString = response.Content.ReadAsStringAsync().Result;
                p = JsonConvert.DeserializeObject<PresenceWork>(resultString);
                return View(p);
            }
            return View(p);
        }

        // GET: PresenceWorks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PresenceWorks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateAttenddance,StatusAttendance,EmployeeId")] PresenceWork presenceWork)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/PresenceWorks", presenceWork);
                response.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "PresenceWorks");
            }
            return View(presenceWork);
        }

        // GET: PresenceWorks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PresenceWork e = null;

            HttpResponseMessage response = await client.GetAsync($"api/PresenceWorks/{id.ToString()}");
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var resultString = response.Content.ReadAsStringAsync().Result;
                e = JsonConvert.DeserializeObject<PresenceWork>(resultString);
                return View(e);
            }
            return View(e);
        }

        // POST: PresenceWorks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateAttenddance,StatusAttendance,EmployeeId")] PresenceWork presenceWork)
        {
            if (id != presenceWork.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    HttpResponseMessage responde = await client.PutAsJsonAsync($"api/PresenceWorks/{id.ToString()}", presenceWork);
                    responde.EnsureSuccessStatusCode();
                    return RedirectToAction("Index", "PresenceWorks");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PresenceWorkExists(presenceWork.Id))
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
            return View(presenceWork);
        }

        // GET: PresenceWorks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PresenceWork e = null;

            HttpResponseMessage response = await client.GetAsync($"api/PresenceWorks/{id.ToString()}");
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var resultString = response.Content.ReadAsStringAsync().Result;
                e = JsonConvert.DeserializeObject<PresenceWork>(resultString);
                return View(e);
            }
            return View(e);
        }

        // POST: PresenceWorks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PresenceWorks == null)
            {
                return Problem("Entity set 'AplicationDBContext.PresenceWorks'  is null.");
            }

            HttpResponseMessage responde = await client.DeleteAsync($"api/PresenceWorks/{id.ToString()}");
            responde.EnsureSuccessStatusCode();
            return RedirectToAction("Index", "PresenceWorks");
        }

        private bool PresenceWorkExists(int id)
        {
          return _context.PresenceWorks.Any(e => e.Id == id);
        }
    }
}
