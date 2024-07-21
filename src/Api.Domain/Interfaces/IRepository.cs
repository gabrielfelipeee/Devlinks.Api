using Api.Domain.Entities;

namespace Api.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity // T deve ser uma classe derivada de BaseEntity
    {
        Task<IEnumerable<T>> SelectAllAsync();
        Task<T> SelectByIdAsync(Guid id);
        Task<T> InsertAsync(T item);
        Task<T> UpdateAsync(T item);
        Task<bool> DeleteAsync(Guid id);
    }
}
