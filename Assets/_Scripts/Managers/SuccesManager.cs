using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace _Scripts.Managers
{
    public class SuccesManager : AutoCleanupSingleton<SuccesManager>
    {
        [SerializeField] private GameObject succesWindow;
        [SerializeField] private TextMeshProUGUI succesHeader;
        [SerializeField] private TextMeshProUGUI succesMessage;
        
        public void TriggerSuccesMessage(string header, string message)
        {
            succesWindow.SetActive(true);
            succesHeader.text = header;
            succesMessage.text = message;
        }

        public void SetActiveFalse()
        {
            succesWindow.SetActive(false);
        }

        public void NextScene(int sceneID)
        {
            SceneManager.LoadScene(sceneID);
        }
    }
}

