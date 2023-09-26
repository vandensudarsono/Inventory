using System.ComponentModel;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Inventory.Models;

public class Assets
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    // [BsonElement("Name")]
    // [JsonPropertyName("Name")]
    public string Name { get; set; } = null!;
    public string Picture {get; set; } = null!;
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public string Editor { get; set; } = null!;
}

public class InventoryDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string AssetsCollectionName { get; set; } = null!;
}