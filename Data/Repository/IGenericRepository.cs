using FriBergs_CarRental.Models.Interfaces;

namespace FriBergs_CarRental.Data
{
    public interface IGenericRepository<T> where T : class, IEntity
    {
        //Create
        void Add(T entity);

        //Read
        T GetById(int EntityId);

        //Update
        void Update(T entity);

        //Delete
        void Delete(T entity);

        //List
        IEnumerable<T> GetAll();


        //Create async
        Task AddAsync(T entity);

        //Read async
        Task<T> GetByIdAsync(int EntityId);

        //Update async
        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        //List Async
        Task<IEnumerable<T>> GetAllAsync();

    }
}
