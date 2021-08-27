using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;


namespace _Scripts.Managers
{
    public class PlayerManager : AutoCleanupSingleton<PlayerManager>
    {
        [SerializeField] private Transform player;

        public Transform Player => player;
    }
}

