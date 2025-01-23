using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Fortune.Repositories.MongoDB.MappedSaveObject
{
    public class MongoDbFortuneModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid MongoId { get { return Id; } } // MongoDB's unique identifier

        [BsonElement("id")]
        public Guid Id { get; set; }

        [BsonElement("longFortune")]
        public string LongFortune { get; set; }

        [BsonElement("shortFortune")]
        public string ShortFortune { get; set; }

        [BsonElement("imageTopics")]
        public string ImageTopics { get; set; }

        [BsonElement("qrImage")]
        public byte[] QrImage { get; set; }

        [BsonElement("audio")]
        public byte[] Audio { get; set; }

        [BsonElement("fortuneImage")]
        public byte[] FortuneImage { get; set; }

        [BsonElement("luckyNumbers")]
        public List<int> LuckyNumbers { get; set; }
        
        [BsonElement("fortuneUsed")]
        public bool FortuneUsed { get; set; }

        [BsonElement("fortuneType")]
        public string FortuneType { get; set; }
    }
}
