using System;
using System.Collections.Generic;
using Google.Cloud.Firestore;
using MongoDB.Bson.Serialization.Attributes;

namespace ExoCortex.Web.Models
{
    [FirestoreData]
    public class InputItem
    {
        public InputItem(){

        }

        public InputItem(string type,Dictionary<string,string> data,DateTime time){
            Type = type;
            Data = data;
            Time = time.ToUniversalTime();
        }

        [FirestoreDocumentId]
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [FirestoreProperty]
        public DateTime Time { get; set; }

        [FirestoreProperty]
        public string Type { get; set; }

        [FirestoreProperty]
        public Dictionary<string, string> Data { get; set; }

        [FirestoreProperty]
        public List<string> HandledBy { get; set; }
    }
}