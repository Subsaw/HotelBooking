using HotelBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace HotelBooking.Controllers
{
    public class BookingController : Controller
    {
        private readonly AppDbContext _context;


        public BookingController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Booking
        public IActionResult Index()
        {
            return View(_context.Bookings);
        }

        public IActionResult Book(int roomId, string roomNumber)
        {
            ViewBag.RoomId = roomId;
            ViewBag.RoomNumber = roomNumber;

            var bookingsForRoom = _context.Bookings
                .Where(b => b.RoomID == roomId)
                .ToList();

            return View(bookingsForRoom);
        }

        public ActionResult Create(int roomID, string roomNumber)
        {
            ViewBag.RoomID = roomID;
            ViewBag.RoomNumber = roomNumber;

            var newBooking = new Booking
            {
                RoomID = roomID,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now
            };

            return View(newBooking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Booking booking)
        {
            int roomID = booking.RoomID;
            var room = _context.Rooms.FirstOrDefault(r => r.RoomID == roomID);
            string roomNumber = room.RoomNumber;

            booking.BookingDate = DateTime.Now;
            

            bool isBooked = _context.Bookings.Any(b => b.RoomID == roomID && !(b.CheckInDate >= booking.CheckOutDate || b.CheckOutDate <= booking.CheckInDate));


            if (isBooked)
            {
                ModelState.AddModelError("", "Room is already booked.");
            }

            if (ModelState.IsValid)
            {
                _context.Bookings.Add(booking);
                _context.SaveChanges();
                return RedirectToAction("Book", new { roomID = roomID, roomNumber = roomNumber});
            }
            else
            {
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        System.Diagnostics.Debug.WriteLine($"Key: {state.Key}, Error: {error.ErrorMessage}");
                    }
                }

                ViewBag.RoomID = roomID;
                ViewBag.RoomNumber = roomNumber;

                return View(booking);
            }
        }

        [HttpGet]
        public JsonResult CheckForOverlap(int roomID, DateTime CheckInDate, DateTime CheckOutDate)
        {
            bool isBooked = _context.Bookings.Any(b => b.RoomID == roomID && 
                ((b.CheckInDate <= CheckInDate && b.CheckOutDate <= CheckOutDate) ||
                (b.CheckInDate < CheckOutDate && b.CheckOutDate >= CheckOutDate)));

            return Json(isBooked);
        }
    }
}
