using FriBergs_CarRental.Models;
using Microsoft.EntityFrameworkCore;

namespace FriBergs_CarRental.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(ApplicationUser user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public async Task AddAsync(ApplicationUser user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public void Delete(ApplicationUser user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public async Task DeleteAsync(ApplicationUser user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public List<ApplicationUser> GetAll()
        {
            return _context.Users.ToList();
        }

        public async Task<List<ApplicationUser>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public ApplicationUser GetById(string id)
        {
            return _context.Users.Find(id);
        }

        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public void Update(ApplicationUser user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public async Task UpdateAsync(ApplicationUser user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
