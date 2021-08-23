using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using TMPro;

namespace _Scripts.Managers
{
    public class ErrorManager : AutoCleanupSingleton<ErrorManager>
    {
        [SerializeField] private GameObject errorWindow;
        [SerializeField] private TextMeshProUGUI errorHeader;
        [SerializeField] private TextMeshProUGUI errorMessage;

        public void TriggerErrorMessage(string header, string message)
        {
            errorWindow.SetActive(true);
            errorHeader.text = header;
            errorMessage.text = message;
        }

        public void SetActiveFalse()
        {
            errorWindow.SetActive(false);
        }
    }
}

