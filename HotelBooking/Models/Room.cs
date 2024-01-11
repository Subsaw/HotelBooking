using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Models
{
    public class Room
    {
        public enum RoomTypes
        {
            Single = 1,
            Double = 2,
            Tripple = 3,
            Quadruple = 4,
            Quintuple = 5
        }

        [Key]
        public int RoomID { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string RoomNumber { get; set; }

        [Column(TypeName = "varchar(50)")]
        public RoomTypes RoomType { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string Description { get; set; }

        [Column(TypeName = "int")]
        public int PricePerNight { get; set; }

    }
}
