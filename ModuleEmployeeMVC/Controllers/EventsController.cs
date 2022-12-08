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
    public class EventsController : Controller
    {
        private readonly AplicationDBContext _context;

        //CONSUMING SERVICES
        static HttpClient client = new HttpClient();

        public EventsController(AplicationDBContext context)
        {
            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri("https://localhost:7164/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }

            _context = context;
        }
        // GET: Events
        public async Task<IActionResult> Index()
        {
            List<Event> list = null;

            HttpResponseMessage response = await client.GetAsync("api/Events");
            if (response.IsSuccessStatusCode)
            {
                var resultString = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<Event>>(resultString);
                client.DefaultRequestHeaders.Clear();
                return View(list.ToList());
            }

            return View(list.ToList());
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            {
                if (id == null || _context.Events == null)
                {
                    return NotFound();
                }
                Event p = null;

                HttpResponseMessage response = await client.GetAsync($"api/Events/{id.ToString()}");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var resultString = response.Content.ReadAsStringAsync().Result;
                    p = JsonConvert.DeserializeObject<Event>(resultString);
                    return View(p);
                }
                return View(p);
            }
        }
        // GET: Events/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,AddressEvent,NameEvent,DateEvent,Status,RegisterDate")] Event @event)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/Events", @event);
                response.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "Events");
            }
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Event e = null;

            HttpResponseMessage response = await client.GetAsync($"api/Events/{id.ToString()}");
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var resultString = response.Content.ReadAsStringAsync().Result;
                e = JsonConvert.DeserializeObject<Event>(resultString);
                return View(e);
            }
            return View(e);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,AddressEvent,NameEvent,DateEvent,Status,RegisterDate")] Event @event)
        {
            if (id != @event.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    HttpResponseMessage responde = await client.PutAsJsonAsync($"api/Events/{id.ToString()}", @event);
                    responde.EnsureSuccessStatusCode();
                    return RedirectToAction("Index", "Events");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.EventId))
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
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Event e = null;

            HttpResponseMessage response = await client.GetAsync($"api/Events/{id.ToString()}");
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var resultString = response.Content.ReadAsStringAsync().Result;
                e = JsonConvert.DeserializeObject<Event>(resultString);
                return View(e);
            }
            return View(e);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Events == null)
            {
                return Problem("Entity set 'DbproductsPracticaContext.Products'  is null.");
            }

            HttpResponseMessage responde = await client.DeleteAsync($"api/Events/{id.ToString()}");
            responde.EnsureSuccessStatusCode();
            return RedirectToAction("Index", "Events");
        }

        private bool EventExists(int id)
        {
          return _context.Events.Any(e => e.EventId == id);
        }
    }
}
