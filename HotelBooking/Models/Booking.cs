using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public virtual Room Room { get; set; }



    }
}
