using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelBooking.Models;

namespace HotelBooking.Models
{
    public class Booking
    {
        [Key]
        public int BookingID { get; set; }

        [ForeignKey("RoomID")]
        public int RoomID { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string GuestName { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime BookingDate { get; set; }

        [Column(TypeName = "date")]
        [CustomValidation(typeof(Booking), nameof(ValidateCheckInDate))]
        public DateTime CheckInDate { get; set; }

        [Column(TypeName = "date")]
        [CustomValidation(typeof(Booking), nameof(ValidateCheckOutDate))]
        public DateTime CheckOutDate { get; set; }

        //[Column(TypeName = "int")]
        //public int TotalPrice { get; set; }

        public static ValidationResult ValidateCheckInDate(DateTime checkInDate)
        {
            if (checkInDate < DateTime.Now)
            {
                return new ValidationResult("Choose your check-in date properly.");
            }
            return ValidationResult.Success;
        }

        public static ValidationResult ValidateCheckOutDate(DateTime CheckOutDate, ValidationContext context)
        {
            var check = context.ObjectInstance as Booking;
            if (check != null && CheckOutDate <= check.CheckInDate)
            {
                return new ValidationResult("Choose your check-out date properly.");
            }
            return ValidationResult.Success;
        }

    }
}
