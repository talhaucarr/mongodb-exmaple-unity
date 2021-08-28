using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Character;
using UnityEngine;
using UnityEngine.Playables;
using _Scripts.Core;

namespace _Scripts.Cinematic
{
    public class CinematicControlRemover : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        private void Start()
        {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
        }

        private void DisableControl(PlayableDirector pd)
        {
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<InputModule>().enabled = false;
            Debug.Log("DisableControl");
        }

        private void EnableControl(PlayableDirector pd)
        {
            player.GetComponent<InputModule>().enabled = true;
            Debug.Log("EnableControl");
        }
    }
}

