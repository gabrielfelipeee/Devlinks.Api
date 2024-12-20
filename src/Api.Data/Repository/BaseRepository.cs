using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DevlinksContext _myContext;
        private readonly DbSet<T> _dataset;
        public BaseRepository(DevlinksContext myContext)
        {
            _myContext = myContext;
            _dataset = _myContext.Set<T>();
        }

        public async Task<IEnumerable<T>> SelectAllAsync()
        {
            return await _dataset.ToListAsync();
        }
        public async Task<T> SelectByIdAsync(Guid id)
        {
            return await _dataset.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<T> InsertAsync(T item)
        {
            if (item.Id == Guid.Empty)
                item.Id = new Guid();

            item.CreatedAt = DateTime.UtcNow;

            await _dataset.AddAsync(item);
            await _myContext.SaveChangesAsync();
            return item;

        }
        public async Task<T> UpdateAsync(Guid id, T item)
        {
            var result = await _dataset.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return null;

            // Preservando a senha original (se for um UserEntity)
            if (result is UserEntity userResult && item is UserEntity userItem)
                userItem.Password = userResult.Password;

            item.Id = result.Id;
            item.CreatedAt = item.CreatedAt;
            item.UpdatedAt = DateTime.UtcNow;
            _myContext.Entry(result).CurrentValues.SetValues(item);
            await _myContext.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _dataset.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return false;

            _dataset.Remove(result);
            await _myContext.SaveChangesAsync();
            return true;
        }
    }
}
