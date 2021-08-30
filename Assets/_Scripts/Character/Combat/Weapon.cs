using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


namespace _Scripts.Character.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon")]
    public class Weapon : ScriptableObject
    {
        [SerializeField] private GameObject weaponPrefab = null;
        [SerializeField] private AnimatorOverrideController animatorOverride = null;
        
        [SerializeField] private float attackRange;
        [SerializeField] private float attackDamage;

        [SerializeField] private bool isRightHanded = true;

        public void Spawn(Transform rightHandTransform, Transform leftHandTransform, Animator animator)
        {
            if (weaponPrefab != null)
            {
                Transform handTransform;
                handTransform = isRightHanded ? rightHandTransform : leftHandTransform;
                
                Instantiate(weaponPrefab, handTransform);
            }
                
            
            if(animatorOverride != null)
                animator.runtimeAnimatorController = animatorOverride;
        }

        public float AttackDamage()
        {
            return attackDamage;
        }

        public float AttackRange()
        {
            return attackRange;
        }
        
    } 
}

