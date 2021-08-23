using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace _Scripts.Database
{
    public class DatabaseConenction : MonoBehaviour
    {
        private MongoClient _client = new MongoClient("mongodb+srv://talha:lara@example.xfait.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");

        private IMongoDatabase _database;

        public IMongoDatabase Database
        {
            get => _database;
        }

        public IMongoDatabase ConnectDatabase(string dbName)
        {
            _database = _client.GetDatabase(dbName);
            return _database;
        }

    }
}

