using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ImageUploadApp.Models
{
    public class ImageTextModel
    {
        //primary key for MongoDB document
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        //stores the text associated with the image
        [BsonElement("Text")]
        public string Text { get; set; } = string.Empty;

        //stores the GridFS Image ID (used to fetch images from MongoDB)
        [BsonElement("ImageId")]
        public string ImageId { get; set; } = string.Empty;

        //used for file upload in the UI
        [BsonIgnore]
        public IFormFile? ImageFile { get; set; }
    }
}
