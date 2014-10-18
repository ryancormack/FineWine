using System;
using MongoDB.Bson.Serialization.Attributes;

namespace FineWine.Domain.Model
{
    public class Wine : IWine
    {
        [BsonId]
        public string Id { get; set; }

        public string Name { get; set; }
        public DateTime TimeAdded { get; set; }
    }
}
