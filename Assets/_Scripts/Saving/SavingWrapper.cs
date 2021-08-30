using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Saving
{
    public class SavingWrapper : MonoBehaviour
    {
        private const string DefaultSaveFile = "save";

        private SavingSystem _savingSystem;

        private void Start()
        {
            _savingSystem = GetComponent<SavingSystem>();
        }

        private void Update()
        {
            
            if(Input.GetKeyDown(KeyCode.S))
                _savingSystem.Save(DefaultSaveFile);
            if(Input.GetKeyDown(KeyCode.L))
                _savingSystem.Load(DefaultSaveFile);
        }
    }
}


