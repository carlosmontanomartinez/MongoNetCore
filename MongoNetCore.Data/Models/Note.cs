using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoNetCore.Data
{
    public class Note
    {
		[BsonId]
		public ObjectId _id { get; set; }
		public string body { get; set; } = string.Empty;
		public DateTime updated_on { get; set; } = DateTime.Now;
		public DateTime created_on { get; set; } = DateTime.Now;
		public int user_id { get; set; } = 0;
    }
}
