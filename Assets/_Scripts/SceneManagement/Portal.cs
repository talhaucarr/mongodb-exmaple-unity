using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.Serialization;


namespace _Scripts.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C, D
        }
        
        [SerializeField] private int sceneIndex = 0;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private DestinationIdentifier destinationIdentifier;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.GetComponent<Tag>().Tags.Contains(Tags.Player)) return;

            StartCoroutine(Transition());

        }

        private IEnumerator Transition()
        {
            DontDestroyOnLoad(gameObject);
            
            yield return SceneManager.LoadSceneAsync(sceneIndex);
            
            var otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);
            
            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            var player = GameObject.FindWithTag("Player");
            player.transform.position = otherPortal.spawnPoint.position;
            
            player.transform.rotation = otherPortal.spawnPoint.rotation;

        }

        private Portal GetOtherPortal()
        {
            foreach (var portal in FindObjectsOfType<Portal>())
            {
                if(portal == this)
                    continue;
                if(portal.destinationIdentifier != destinationIdentifier) 
                    continue;

                return portal;
            }

            return null;
        }
    }
}

