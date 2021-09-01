using System.Collections;
using System.Collections.Generic;
using _Scripts.Character.Vitality;
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
        [SerializeField] private Projectile projectile = null;

        private const string WeaponName = "Weapon";

        public void Spawn(Transform rightHandTransform, Transform leftHandTransform, Animator animator)
        {
            DestroyOldWeapon(rightHandTransform, leftHandTransform);
            
            if (weaponPrefab != null)
            {
                
                Transform handTransform = GetTransform(rightHandTransform, leftHandTransform);
                GameObject weapon = Instantiate(weaponPrefab, handTransform);
                weapon.name = WeaponName;
            }

            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            
            if (animatorOverride != null)
                animator.runtimeAnimatorController = animatorOverride;

            else if(overrideController != null)
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            

        }

        private void DestroyOldWeapon(Transform rightHandTransform, Transform leftHandTransform)
        {
            Transform oldWeapon = rightHandTransform.Find(WeaponName);
            if (oldWeapon == null)
            {
                oldWeapon = leftHandTransform.Find(WeaponName);
            }
            if(oldWeapon == null) return;
            
            Destroy(oldWeapon.gameObject);
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void LaunchProjectile(Transform rightHandTransform, Transform leftHandTransform, Health target)
        {
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHandTransform, leftHandTransform).position, Quaternion.identity);
            projectileInstance.SetTarget(target, attackDamage);
        }

        public float AttackDamage()
        {
            return attackDamage;
        }

        public float AttackRange()
        {
            return attackRange;
        }

        private Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            
            return isRightHanded ? rightHand : leftHand;
        }
        
    } 
}

