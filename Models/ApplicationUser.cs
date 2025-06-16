using FriBergs_CarRental.Models.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace FriBergs_CarRental.Models
{
    public class ApplicationUser : IdentityUser<int>, IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //Ifall jag har en customer role måste den ha en nav prop till CustomerORder
        public ICollection<CustomerOrder> CustomerOrder { get; set; } = new List<CustomerOrder>();
 
    }
}
