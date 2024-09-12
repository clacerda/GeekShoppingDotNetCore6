using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GeekShopping.ProductAPI.Data.ValueObjects
{
    public class ImageVO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _Id { get; set; }

        [BsonElement("id_product")]
        public string? IdProduct { get; set; }

        [BsonElement("baseimage")]
        public string? BaseImage { get; set; }
    }
}
