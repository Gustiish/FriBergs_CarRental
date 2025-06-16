using FriBergs_CarRental.Models.Interfaces;

namespace FriBergs_CarRental.Models
{
    public class CustomerOrder : IEntity
    {
        public int Id { get; set; }
        public Car Car { get; set; }
        public int CarId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Price { get; set; }
        public ApplicationUser Customer { get; set; }
        public int CustomerId { get; set; }
    }
}
