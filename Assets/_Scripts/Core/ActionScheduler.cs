using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Core
{
    public class ActionScheduler : MonoBehaviour
    {

        private MonoBehaviour _currentAction;
        public void StartAction(MonoBehaviour action)
        {
            Debug.Log("canceling action:" + action);
            _currentAction = action;
            Debug.Log("current action:" + _currentAction);
        }
    }
}
