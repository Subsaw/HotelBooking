using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Models
{
    public class Activityevent
    {
        [Key]
        public int ActivityID { get; set; }

        [Column(TypeName = "datetime")]
        [CustomValidation(typeof(Booking), nameof(ValidateCheckInDate))]
        public DateTime DateWhen { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Title { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Description { get; set; }

        [Column(TypeName = "int")]
        public int Price { get; set; }

        public static ValidationResult ValidateCheckInDate(DateTime dateWhen)
        {
            if (dateWhen < DateTime.Now)
            {
                return new ValidationResult("Choose date properly.");
            }
            return ValidationResult.Success;
        }
    }
}
