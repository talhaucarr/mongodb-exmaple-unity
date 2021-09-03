using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using _Scripts.Character.Vitality;


namespace _Scripts.Enemy
{
    public class EnemyHeadDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshPro headDisplayText;

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
            headDisplayText.text = $"Enemy: {_health.GetPercentHealth()}%";
        }
    }
}

