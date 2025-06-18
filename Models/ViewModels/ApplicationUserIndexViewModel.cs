namespace FriBergs_CarRental.Models.ViewModels
{
    public class ApplicationUserIndexViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public ICollection<CustomerOrder> CustomerOrders { get; set; } = new List<CustomerOrder>();

    }
}
