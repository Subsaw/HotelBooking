using HotelBooking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace HotelBooking.Controllers
{
    public class RoomController : Controller
    {

        private static List<Room> list = new()
        {
            new Room { RoomID = 1, RoomNumber = "504", RoomType = "Single", Description = "Alalsdl", PricePerNight = 50},
            new Room { RoomID = 2, RoomNumber = "104", RoomType = "Single", Description = "ASdkaskdkasd", PricePerNight = 60},
            new Room { RoomID = 3, RoomNumber = "28", RoomType = "Double", Description = "AJKshdajhsd", PricePerNight = 90},
            new Room { RoomID = 4, RoomNumber = "128", RoomType = "Double", Description = "ASIDJOQIWD", PricePerNight = 120},
            new Room { RoomID = 5, RoomNumber = "198", RoomType = "Triple", Description = "JAHSdKAJSdoAOIJS", PricePerNight = 160}
        };

        // GET: RoomController
        public ActionResult Index()
        {
            return View(list);
        }

        // GET: RoomController/Details/5
        public ActionResult Details(int id)
        {
            return View(list.FirstOrDefault(x => x.RoomID == id));
        }

        // GET: RoomController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoomController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Room rooms)
        {
            try
            {
                rooms.RoomID = list.Count + 1;
                list.Add(rooms);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: RoomController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(list.FirstOrDefault(x => x.RoomID == id));
        }

        // POST: RoomController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Room rooms)
        {
            try
            {
                Room room1 = list.FirstOrDefault(x => x.RoomID == id);
                room1.RoomNumber = rooms.RoomNumber;
                room1.RoomType = rooms.RoomType;
                room1.Description = rooms.Description;
                room1.PricePerNight = rooms.PricePerNight;

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: RoomController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(list.FirstOrDefault(x => x.RoomID == id));
        }

        // POST: RoomController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Room room)
        {
            try
            {
                Room room1 = list.FirstOrDefault(x => x.RoomID == id);
                list.Remove(room1);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
