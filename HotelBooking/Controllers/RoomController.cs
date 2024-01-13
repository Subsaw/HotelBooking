using HotelBooking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Data;
using System.Diagnostics;

namespace HotelBooking.Controllers
{
    public class RoomController : Controller
    {
        private readonly AppDbContext _context;
        public RoomController(AppDbContext context)
        {
            _context = context;
        }

        // GET: RoomController
        public ActionResult Index()
        {
            return View(_context.Rooms);
        }

        // GET: RoomController/Details/5
        public ActionResult Details(int id)
        {
            return View(_context.Rooms.Find(id));
        }

        // GET: RoomController/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoomController/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Room room)
        {
                _context.Rooms.Add(room);
                _context.SaveChanges();
                return RedirectToAction("Index");   
        }

        // GET: RoomController/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            return View(_context.Rooms.Find(id));
        }

        // POST: RoomController/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Room room)
        {
            var RoomForEdit = _context.Rooms.Find(id);
            RoomForEdit.RoomNumber = room.RoomNumber;
            RoomForEdit.RoomType = room.RoomType;
            RoomForEdit.Description = room.Description;
            RoomForEdit.PricePerNight = room.PricePerNight;

            _context.Update(RoomForEdit);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: RoomController/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            return View(_context.Rooms.Find(id));
        }

        // POST: RoomController/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Room room)
        {
            var RoomToDelete = _context.Rooms.Find(id);
            _context.Rooms.Remove(RoomToDelete);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
