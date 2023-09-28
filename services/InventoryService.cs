using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Inventory.Models;

namespace Inventory.Services;

public class AssetsService
{
    private readonly IMongoCollection<Assets> _assetsCollection;

    public AssetsService (
        IOptions<InventoryDatabaseSettings> inventoryDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            inventoryDatabaseSettings.Value.ConnectionString
        );

        var mongoDatabase = mongoClient.GetDatabase(
            inventoryDatabaseSettings.Value.DatabaseName
        );

        _assetsCollection = mongoDatabase.GetCollection<Assets>(
            inventoryDatabaseSettings.Value.AssetsCollectionName
        );
    }   
    
    public async Task<List<Assets>> GetAsync() => 
        await _assetsCollection.Find(_ => true).ToListAsync();

    public async Task<Assets?> GetOneAsync(string id) => 
        await _assetsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateOneAsync(Assets newAsset) =>
        await _assetsCollection.InsertOneAsync(newAsset);

    public async Task UpdateOneAsync(string id, Assets updateAssets) =>
        await _assetsCollection.ReplaceOneAsync(x => x.Id == id, updateAssets);

    public async Task RemoveOneAsync(string id) => 
        await _assetsCollection.DeleteOneAsync(x => x.Id == id);
    
}