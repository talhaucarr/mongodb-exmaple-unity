using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Character.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] private Weapon weapon = null;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.GetComponent<Tag>().Tags.Contains(Tags.Player)) return;
            
            other.GetComponent<AttackModule>().EquipWeapon(weapon);
            Destroy(gameObject);

        }
    }

}
