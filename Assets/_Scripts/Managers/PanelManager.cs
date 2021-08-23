using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;


namespace _Scripts.Managers
{
    public class PanelManager : AutoCleanupSingleton<PanelManager>
    {
        [SerializeField] private GameObject loginWindow;
        [SerializeField] private GameObject registerWindow;

        public void SetActiveLoginWindow()
        {
            loginWindow.SetActive(true);
            registerWindow.SetActive(false);
        }

        public void SetActiveRegisterWindow()
        {
            registerWindow.SetActive(true);
            loginWindow.SetActive(false);
        }
    }
}


