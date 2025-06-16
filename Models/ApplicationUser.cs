using FriBergs_CarRental.Models.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace FriBergs_CarRental.Models
{
    public class ApplicationUser : IdentityUser<int>, IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<CustomerOrder>? CustomerOrders { get; set; } 
        //Om endast customer ska ha tillgång till CustomerOrders behövs det inte nav prop för annat.
    }
}
