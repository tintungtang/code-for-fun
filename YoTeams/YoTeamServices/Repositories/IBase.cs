namespace YoTeamServices.Repositories;

using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBaseRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(string id);
    Task CreateAsync(T entity);
    Task<bool> UpdateAsync(string id, T entity);
    Task<bool> DeleteAsync(string id);
}
