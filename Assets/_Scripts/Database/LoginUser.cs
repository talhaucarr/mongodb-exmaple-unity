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
                        return true;
                }
                    
            }
            return false;
        }
    }
}

