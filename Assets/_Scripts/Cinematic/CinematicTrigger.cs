using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


namespace _Scripts.Cinematic
{
    public class CinematicTrigger : MonoBehaviour
    {
        private bool _isTrigger = false;
        private void OnTriggerEnter(Collider other)
        {
            if(_isTrigger) return;

            if (!other.gameObject.GetComponent<Tag>().Tags.Contains(Tags.Player)) return;
            
            GetComponent<PlayableDirector>().Play();
            _isTrigger = true;
        }
    }
}

