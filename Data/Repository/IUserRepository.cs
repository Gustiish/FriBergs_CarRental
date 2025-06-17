using FriBergs_CarRental.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace FriBergs_CarRental.Data.Repository
{
    public interface IUserRepository
    {
        //Create
        void Add(ApplicationUser user);


        //Read
        ApplicationUser GetById(string Id);

        //Update
        void Update(ApplicationUser user);

        //Delete
        void Delete(ApplicationUser user);

        //GetAll
        IEnumerable<ApplicationUser> GetAll();

        Task AddAsync(ApplicationUser user);

        Task<ApplicationUser> GetByIdAsync(string Id);

        Task UpdateAsync(ApplicationUser user);

        Task DeleteAsync(ApplicationUser user);

        Task<IEnumerable<ApplicationUser>> GetAllAsync();







    }
}
