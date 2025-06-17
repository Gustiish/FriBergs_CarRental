using FriBergs_CarRental.Models.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace FriBergs_CarRental.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //Ifall jag har en customer role måste den ha en nav prop till CustomerORder
        public virtual ICollection<CustomerOrder> CustomerOrders { get; set; } = new List<CustomerOrder>();
 
    }
}
