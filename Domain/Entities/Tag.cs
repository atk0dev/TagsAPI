namespace Domain.Entities
{
    using Domain.Common;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Tag : TrackableEntity, IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }
    }
}
