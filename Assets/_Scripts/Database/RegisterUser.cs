using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using TMPro;
using _Scripts.Managers;

namespace _Scripts.Database
{
    public class RegisterUser : MonoBehaviour
    {
        [SerializeField] private TMP_InputField username;
        [SerializeField] private TMP_InputField password;
        [SerializeField] private TMP_InputField email;

        public void Register()
        {
            if (IsUserExists())
            {
                ErrorManager.Instance.TriggerErrorMessage("Hata","Kullanici adi daha once alinmis!");
                return;
            }

            User user = new User
            {
                Username = username.text,
                Password = password.text,
                Email = email.text,
                Informations = new UserInformations
                {
                    Level = 1,
                    Exp = 0,
                    Gold = 0
                },
                Stats = new UserStats
                {
                    StatPoints = 3,
                    HP = 100,
                    STR = 10,
                    DEX = 10,
                    VIT = 10
                }
                
            };

            DatabaseOperations.Instance.InsertRecord("UserCollection", user);
            PanelManager.Instance.SetActiveLoginWindow();
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
    }

    public class User
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Username { get; set; }
    
        public string Password { get; set; }
    
        public string Email { get; set; }

        public UserInformations Informations { get; set; }
        public UserStats Stats { get; set; }
    }

    public class UserInformations
    {
        public int Level { get; set; }
        public int Exp { get; set; }
        public int Gold { get; set; }
    }

    public class UserStats
    {
        public int StatPoints { get; set; }
        public int HP { get; set; }
        public int STR { get; set; }
        public int DEX { get; set; }
        public int VIT { get; set; }
    }
}
