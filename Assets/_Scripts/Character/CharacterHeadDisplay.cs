using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using _Scripts.Character.Vitality;
using Microsoft.SqlServer.Server;


namespace _Scripts.Character
{
    public class CharacterHeadDisplay : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI usernameText;

        private Health _health;

        private void Start()
        {
            _health = GetComponent<Health>();
        }

        private void Update()
        {
            UpdateHealthText();
        }

        private void UpdateHealthText()
        {
            usernameText.text = $"{_health.GetPercentHealth()}%";
        }
    }

}
