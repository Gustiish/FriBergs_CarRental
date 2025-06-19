namespace FriBergs_CarRental.Models.ViewModels
{
    public class CreateCustomerOrderViewModel
    {
        public string Id { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public int Price { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }





    }
}
