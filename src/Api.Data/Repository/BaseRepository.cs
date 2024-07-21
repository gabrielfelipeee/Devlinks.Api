using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly MyContext _myContext;
        private readonly DbSet<T> _dataset;
        public BaseRepository(MyContext myContext)
        {
            _myContext = myContext;
            _dataset = _myContext.Set<T>();
        }

        public async Task<IEnumerable<T>> SelectAllAsync()
        {
            try
            {
                var result = await _dataset.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> SelectByIdAsync(Guid id)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(x => x.Id == id);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<T> InsertAsync(T item)
        {
            try
            {
                if (item.Id == Guid.Empty)
                {
                    item.Id = new Guid();
                }
                item.CreatedAt = DateTime.UtcNow;

                await _dataset.AddAsync(item);
                await _myContext.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<T> UpdateAsync(T item)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(x => x.Id == item.Id);
                if (result == null)
                {
                    return null;
                }

                // Usando reflexão para preservar a senha antiga
                var passwordProperty = typeof(T).GetProperty("Password"); // Obtém a propriedade "Password" do tipo T, retornando um objeto PropertyInfo
                if (passwordProperty != null)
                {
                    var existingPassword = passwordProperty.GetValue(result); // Obtém o valor atual da propriedade "Password" do objeto result (senha antiga)
                    passwordProperty.SetValue(item, existingPassword);  // Define o valor da propriedade "Password" do objeto item com a senha antiga
                }

                item.Id = result.Id;
                item.Id = result.Id;
                item.CreatedAt = item.CreatedAt;
                item.UpdatedAt = DateTime.UtcNow;

                _myContext.Entry(result).CurrentValues.SetValues(item);
                await _myContext.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(x => x.Id == id);
                if (result == null)
                {
                    return false;
                }
                _dataset.Remove(result);
                await _myContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}