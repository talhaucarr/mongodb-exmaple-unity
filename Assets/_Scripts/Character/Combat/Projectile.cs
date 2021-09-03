using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using _Scripts.Character.Vitality;
using UnityEngine;

namespace _Scripts.Character.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed = 1;
        [SerializeField] private bool isHoming = true;
        [SerializeField] private GameObject hitEffect = null;
        [SerializeField] private float maxLifeTime = 10;
        [SerializeField] private GameObject[] destroyOnHit = null;
        [SerializeField] private float lifeAfterImpact = 2;
        
        private Health _target = null;
        private GameObject _instigator = null;
        private float _damage = 0;

        private void Start()
        {
            transform.LookAt(AimLocation());
        }

        private void Update()
        {
            if(_target == null) return;

            if (isHoming && !_target.IsDead())
            {
                transform.LookAt(AimLocation());
            }
                
            
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void SetTarget(Health target, GameObject instigator, float damage)
        {
            this._target = target;
            this._damage = damage;
            this._instigator = instigator;
            
            Destroy(gameObject, maxLifeTime);
        }

        private Vector3 AimLocation()
        {
            CapsuleCollider targetCollider = _target.GetComponent<CapsuleCollider>();
            if (targetCollider == null)
                return _target.transform.position;
            return _target.transform.position + Vector3.up * targetCollider.height / 2;//body shot
            //for headshot: return target.position + Vector3.up * targetCollider.height;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<Health>() != _target) return;
            if(_target.IsDead()) return;
            _target.TakeDamage(_instigator ,_damage);
            
            speed = 0;
            
            if (hitEffect != null)
            {
                Instantiate(hitEffect, AimLocation(), transform.rotation);
            }

            foreach (GameObject toDestroy in destroyOnHit)
            {
                Destroy(toDestroy);
            }
            
            Destroy(gameObject, lifeAfterImpact);
        }
    }
}

