using MongoDB.Bson.Serialization.Attributes;

namespace FineWine.Domain.Model
{
    public interface IWine
    {
        [BsonId]
        string Id { get; set; }

        string Name { get; set; }
    }
}