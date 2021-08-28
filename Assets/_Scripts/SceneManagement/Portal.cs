using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;


namespace _Scripts.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private int sceneIndex = 0;
        [SerializeField] private Transform spawnPoint;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.GetComponent<Tag>().Tags.Contains(Tags.Player)) return;

            StartCoroutine(Transition());

        }

        private IEnumerator Transition()
        {
            DontDestroyOnLoad(gameObject);
            
            yield return SceneManager.LoadSceneAsync(sceneIndex);
            
            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);
            
            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(player.transform.position = otherPortal.spawnPoint.position);
            
            player.transform.rotation = otherPortal.spawnPoint.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if(portal == this)
                    continue;

                return portal;
            }

            return null;
        }
    }
}

