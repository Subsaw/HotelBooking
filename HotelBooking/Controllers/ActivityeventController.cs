using HotelBooking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
    public class ActivityeventController : Controller
    {

        private readonly AppDbContext _context;

        public ActivityeventController(AppDbContext context)
        {
            _context = context;
        }


        // GET: ActivityController
        public ActionResult Index()
        {
            return View(_context.Activityevents);
        }

        // GET: ActivityController/Details/5
        public ActionResult Details(int id)
        {
            return View(_context.Activityevents.Find(id));
        }

        // GET: ActivityController/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActivityController/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Activityevent activityevents)
        {
            _context.Activityevents.Add(activityevents);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: ActivityController/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            return View(_context.Activityevents.Find(id));
        }

        // POST: ActivityController/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Activityevent activity)
        {
            var activityForEdit = _context.Activityevents.Find(id);
            activityForEdit.DateWhen = activity.DateWhen;
            activityForEdit.Price = activity.Price;
            activityForEdit.Title = activity.Title;
            activityForEdit.Description = activity.Description;

            _context.Update(activityForEdit);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: ActivityController/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            return View(_context.Activityevents.Find(id));
        }

        // POST: ActivityController/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Activityevent activity)
        {
            var activityToDelete = _context.Activityevents.Find(id);
            _context.Activityevents.Remove(activityToDelete);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
