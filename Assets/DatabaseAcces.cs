using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using UnityEngine;

public class DatabaseAcces : MonoBehaviour
{
    
    private MongoClient _client = new MongoClient("mongodb+srv://talha:lara@example.xfait.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
    
    private IMongoDatabase _database;
    private IMongoCollection<BsonDocument> _collection;


    void Start()
    {
        _database = _client.GetDatabase(("ChatroomDB"));
        _collection = _database.GetCollection<BsonDocument>("ChatroomCollection");


    }

    public async void Test(string username, int score)
    {
        var document = new BsonDocument{{username, score}};
        await _collection.InsertOneAsync(document);
    }

    private async Task<List<HighScore>> GetScoresFromDataBase()
    {
        var allScoresTask = _collection.FindAsync((new BsonDocument()));
        var scoresAwaited = await allScoresTask;

        List<HighScore> highScores = new List<HighScore>();
        foreach (var score in scoresAwaited.ToList())
        {
            highScores.Add(Deserialize(score.ToString()));
        }

        return highScores;
    }

    private HighScore Deserialize(string toString)
    {
        var highScore = new HighScore();

        var stringWitoutID = toString.Substring(toString.IndexOf("),") + 4);
        var username = toString.Substring(0, stringWitoutID.IndexOf(":") - 2);

        var score = stringWitoutID.Substring(stringWitoutID.IndexOf(":") + 2, stringWitoutID.IndexOf("}") - stringWitoutID.IndexOf(":") - 3);
        highScore.Username = username;
        highScore.Score = Convert.ToInt32(score);
        return highScore;
    }
}

public class HighScore
{
    public string Username { get; set; }
    public int Score { get; set; }
}
