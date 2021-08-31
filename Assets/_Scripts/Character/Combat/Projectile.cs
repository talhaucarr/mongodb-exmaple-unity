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
        
        private Health _target = null;
        private float _damage = 0;

        private void Update()
        {
            if(_target == null) return;
            
            transform.LookAt(AimLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void SetTarget(Health target, float damage)
        {
            this._target = target;
            this._damage = damage;
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
            Debug.Log("here");
            _target.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}

