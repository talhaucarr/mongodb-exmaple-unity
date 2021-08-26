using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Scripts.Character.Combat;
using Photon.Pun;


namespace _Scripts.Character
{
    public class InputModule : MonoBehaviour
    {
        private IMovementModule _movementModule;
        private IAttackModule _attackModule;
        private PhotonView _pw;
        private void Start()
        {
            _movementModule = GetComponent<MovementModule>();
            _attackModule = GetComponent<AttackModule>();
            _pw = GetComponent<PhotonView>();
        }
        private void Update()
        {
            if (_pw.IsMine)
            {
                if (InteractWithCombat())
                    return;
                if (InteractWithMovement())
                    return;
            }
            
            
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                if (!hit.transform.gameObject.GetComponent<Tag>().Tags.Contains(Tags.Enemy))
                    continue;

                GameObject enemy = hit.transform.gameObject;
                if(Input.GetMouseButtonDown(0))
                {
                    _attackModule.Attack(enemy);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {      
            Ray ray = GetMouseRay();
            bool hasHit = Physics.Raycast(ray, out RaycastHit hit);

            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    _movementModule.StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        
    }

}
