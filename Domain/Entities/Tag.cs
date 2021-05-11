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

        [BsonElement("TagID")]
        public string TagID { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("SelfAssign")]
        public bool SelfAssign { get; set; }

        [BsonElement("RequiresOnboarding")]
        public bool RequiresOnboarding { get; set; }

        [BsonElement("IsArchived")]
        public bool IsArchived { get; set; }
    }
}