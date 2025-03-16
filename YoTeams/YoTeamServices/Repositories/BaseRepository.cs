namespace YoTeamServices.Repositories;

using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

public class BaseRepository<T> : IBaseRepository<T>
{
    private readonly IMongoCollection<T> _collection;

    public BaseRepository(IMongoDatabase database, string collectionName)
    {
        _collection = database.GetCollection<T>(collectionName);
    }

    public async Task<IEnumerable<T>> GetAllAsync() =>
        await _collection.Find(_ => true).ToListAsync();

    public async Task<T> GetByIdAsync(string id) =>
        await _collection.Find(Builders<T>.Filter.Eq("_id", id)).FirstOrDefaultAsync();

    public async Task CreateAsync(T entity) =>
        await _collection.InsertOneAsync(entity);

    public async Task<bool> UpdateAsync(string id, T entity)
    {
        var result = await _collection.ReplaceOneAsync(
            Builders<T>.Filter.Eq("_id", id), entity);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", id));
        return result.DeletedCount > 0;
    }
}
