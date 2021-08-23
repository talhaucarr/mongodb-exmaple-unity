using UnityEngine;

namespace Utilities
{
    /// <summary>
    /// Singleton pattern with auto destroy
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AutoCleanupSingleton<T> : MonoBehaviourWithLog
        where T : MonoBehaviourWithLog
    {
        #region Fields

        /// <summary>
        /// The instance.
        /// </summary>
        protected static T _instance;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;

                _instance = FindObjectOfType<T>();
                if (_instance == null)
                    _instance = new GameObject("Instance of" + typeof(T)).AddComponent<T>();

                return _instance;
            }
        }

        #endregion

        #region Methods

        protected virtual void Awake()
        {
            if (_instance != null)
            {
                Debug.Log($"[{this.GetType().Name}]: Not null, destroy");
                Destroy(gameObject);
            }
        }

        #endregion
    }
}