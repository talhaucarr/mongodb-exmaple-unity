using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using TMPro;
using _Scripts.Managers;

public class RegisterUser : MonoBehaviour
{
    [SerializeField] private TMP_InputField username;
    [SerializeField] private TMP_InputField password;
    [SerializeField] private TMP_InputField email;

    [SerializeField] private GameObject errorWindow;
    
    public void Register()
    {
        if (IsUserExists())
        {
            ErrorManager.Instance.TriggerErrorMessage("Hata","Kullanici adi daha once alinmis!");
            return;
        }
        DatabaseOperations.Instance.InsertRecord("UserCollection",new User{Username = username.text, Password = password.text, Email = email.text});
    }

    private bool IsUserExists()
    {
        var records = DatabaseOperations.Instance.LoadRecords<User>("UserCollection");

        foreach (var record in records)
        {
            if(record.Username == username.text)
                return true;
        }

        return false;
    }

    private void ErrorWindowHandler()
    {
        
    }
}

public class User
{
    [BsonId]
    public Guid Id { get; set; }
    public string Username { get; set; }
    
    public string Password { get; set; }
    
    public string Email { get; set; }
}