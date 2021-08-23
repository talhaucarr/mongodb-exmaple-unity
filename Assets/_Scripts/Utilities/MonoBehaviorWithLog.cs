using UnityEngine;

namespace Utilities
{
    public class MonoBehaviourWithLog : MonoBehaviour
    {
        #region Fields

        /// <summary>
        /// Allow debug
        /// </summary>
        [SerializeField] protected bool debug = true;

        #endregion

        protected virtual void Log(string msg)
        {
            if (!debug) return;
            Debug.Log($"[{GetType().Name}]: {msg}");
        }

        protected virtual void LogWarning(string msg)
        {
            if (!debug) return;
            Debug.LogWarning($"[{GetType().Name}]: {msg}");
        }
    }
}