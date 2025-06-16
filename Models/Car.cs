using FriBergs_CarRental.Models.Interfaces;

namespace FriBergs_CarRental.Models
{
    public class Car : IEntity
    {
        public int Id { get; set; }
        public List<string> Images { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public CustomerOrder? CustomerOrder { get; set; }
        
    }
}
