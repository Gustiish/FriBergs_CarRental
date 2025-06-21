using System.ComponentModel.DataAnnotations;

namespace FriBergs_CarRental.Models.ViewModels
{
    public class CreateCustomerOrderViewModel
    {
        public int CarId { get; set; }

        public Car? Car { get; set; }
        public int Price { get; set; }
        [Required(ErrorMessage = "Startdatum krävs")]
        public DateOnly StartTime { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        [Required(ErrorMessage = "Slutdatum krävs")]
        public DateOnly EndTime { get; set; } = DateOnly.FromDateTime(DateTime.Now);





    }
}
