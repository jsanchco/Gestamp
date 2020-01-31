namespace SGDE.DataEFCoreMongoDB.Models
{
    #region Using

    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    #endregion

    public class Account
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        [BsonElement("user")]
        public string user { get; set; }

        [BsonElement("provider")]
        public string provider { get; set; }

        [BsonElement("defaultAccount")]
        public bool defaultAccount { get; set; }

        public int __v { get; set; }
    }
}
