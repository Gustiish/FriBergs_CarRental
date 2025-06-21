using FriBergs_CarRental.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace FriBergs_CarRental.Models
{
    public class CustomerOrder : IEntity
    {
        [Key]
        public int Id { get; set; }
        public Car Car { get; set; }
        public int CarId { get; set; }
        public DateOnly StartTime { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly EndTime { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public int Price { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }



    }
}
