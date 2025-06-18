using FriBergs_CarRental.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FriBergs_CarRental.Models
{
    public class Car : IEntity
    {
        [Key]
        public int Id { get; set; }
        public List<string> Images { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public CustomerOrder? CustomerOrder { get; set; }
        
    }
}
