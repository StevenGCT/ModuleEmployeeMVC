using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ModuleEmployeeMVC.Controllers
{
    public class ScheduleController1 : Controller
    {
        // GET: ScheduleController1
        public ActionResult Index()
        {
            return View();
        }

        // GET: ScheduleController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ScheduleController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ScheduleController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ScheduleController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ScheduleController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ScheduleController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ScheduleController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
