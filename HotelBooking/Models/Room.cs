namespace HotelBooking.Models
{
    public class Room
    {
        public int RoomID { get; set; }
        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
        public string Description { get; set; }
        public int PricePerNight { get; set; }
    }
}
