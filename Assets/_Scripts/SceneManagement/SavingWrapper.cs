using System.Collections;
using System.Collections.Generic;
using _Scripts.Saving;
using UnityEngine;

namespace _Scripts.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        private const string SaveFile = "save";
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
                Save();
            
            if(Input.GetKeyDown(KeyCode.L))
                Load();
        }

        private void Save()
        {
            GetComponent<SavingSystem>().Save(SaveFile);
        }

        private void Load()
        {
            GetComponent<SavingSystem>().Load(SaveFile);
        }
    }
}

