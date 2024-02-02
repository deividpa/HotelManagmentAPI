using System.Linq.Expressions;

namespace HotelAPI.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task Create(T entity);
        Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null);
        Task<T> GetOne(Expression<Func<T, bool>>? filter = null, bool tracked=true);
        Task Remove(T entity);
        Task Save();

    }
}
