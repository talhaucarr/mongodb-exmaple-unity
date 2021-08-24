using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using _Scripts.Managers;

namespace _Scripts.Database
{
    public class LoginUser : MonoBehaviour
    {
        
        [SerializeField] private TMP_InputField username;
        [SerializeField] private TMP_InputField password;

        public void Login()
        {
            if(!IsUserExists())
            {
                ErrorManager.Instance.TriggerErrorMessage("Giris Hatasi","Kullanici adi veya sifre hatali!");
                return;
            }
            SuccesManager.Instance.TriggerSuccesMessage("Giris", "Kullanici adi ve parola dogru!");
        }

        private bool IsUserExists()
        {
            var records = DatabaseOperations.Instance.LoadRecords<User>("UserCollection");

            foreach (var record in records)
            {
                if (record.Username == username.text)
                {
                    if(record.Password == password.text)
                    {
                        UpdateStatsManager(record);
                        return true;
                    }
                        
                }
                    
            }
            return false;
        }

        private void UpdateStatsManager(User user)
        {
            StatsManager.Instance.Username = user.Username;
            StatsManager.Instance.Level = user.Informations.Level;
            StatsManager.Instance.Exp = user.Informations.Exp;

            StatsManager.Instance.Gold = user.Informations.Gold;

            StatsManager.Instance.StatPoints = user.Stats.StatPoints;
            StatsManager.Instance.HP = user.Stats.HP;
            StatsManager.Instance.STR = user.Stats.STR;
            StatsManager.Instance.DEX = user.Stats.DEX;
            StatsManager.Instance.VIT = user.Stats.VIT;
        }

    }
}

