using Dela.Toolkit.Domain; 
using Humanizer;
using MongoDB.Driver;
namespace Dela.Toolkit.Persistence.Mongo;

public class MongoRepository<T,TKey> where T: AggregateRoot<TKey>
{
    protected IMongoCollection<T> AggregateCollection {get; private set;}

    public MongoRepository(IMongoDatabase database)
    {
        
        AggregateCollection = database.GetCollection<T>(typeof(T).Name.Pluralize());
    }
    
    public Task AddAsync(T item)
    { 
        return AggregateCollection.InsertOneAsync(item);
    }

    public Task<T> GetByIdAsync(TKey id)
    { 
        return AggregateCollection.Find(a => a.Id.Equals(id)).FirstOrDefaultAsync();
    }
}