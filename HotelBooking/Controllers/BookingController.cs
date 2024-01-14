using HotelBooking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace HotelBooking.Controllers
{
    public class BookingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public BookingController(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Booking
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View(_context.Bookings);
        }

        [Authorize]
        public async Task<IActionResult> Book(int roomId, string roomNumber)
        {
            ViewBag.RoomId = roomId;
            ViewBag.RoomNumber = roomNumber;

            var bookingsForRoom = await _context.Bookings
                .Include(b => b.Room)
                .Where(b => b.RoomID == roomId)
                .ToListAsync();

            return View(bookingsForRoom);
        }

        [Authorize]
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
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Booking booking)
        {
            int roomID = booking.RoomID;
            var room = _context.Rooms.FirstOrDefault(r => r.RoomID == roomID);
            string roomNumber = room.RoomNumber;

            booking.BookingDate = DateTime.Now;
            booking.GuestName = _userManager.GetUserName(User);
            booking.UserId = _userManager.GetUserId(User);
            booking.Room = room;
            

            bool isBooked = _context.Bookings.Any(b => b.RoomID == roomID && !(b.CheckInDate >= booking.CheckOutDate || b.CheckOutDate <= booking.CheckInDate));


            if (isBooked)
            {
                ModelState.AddModelError("", "Room is already booked.");
            }

            if (ModelState.IsValid)
            {
                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction("MyBookings");
            }
            else
            {
                ViewBag.RoomID = roomID;
                ViewBag.RoomNumber = roomNumber;

                return View(booking);
            }
        }

        [Authorize]
        public async Task<IActionResult> MyBookings()
        {
            var userId = _userManager.GetUserId(User);
            var bookings = await _context.Bookings
                .Where(b => b.UserId == userId)
                .ToListAsync();

            return View(bookings);
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
