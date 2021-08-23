using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using UnityEngine;
using Utilities;


public class DatabaseOperations : AutoCleanupSingleton<DatabaseOperations>
{
    private DatabaseConenction _dbConnection;

    private IMongoDatabase _db;

    private void Start()
    {
        _dbConnection = GetComponent<DatabaseConenction>();
        
        _db = _dbConnection.ConnectDatabase("ChatroomDB");

        //InsertRecord("UserCollection", new User{Username = "talla", Password = "talla41", Email = "sad@hotmail.com"});
    }

    public void InsertRecord<T>(string table, T record)
    {
        var collection = _db.GetCollection<T>(table);
        collection.InsertOne(record);
    }

    public List<T> LoadRecords<T>(string table)
    {
        var collection = _db.GetCollection<T>(table);
        return collection.Find(new BsonDocument()).ToList();
    }
}


