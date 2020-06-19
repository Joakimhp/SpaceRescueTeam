using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace utils.singleton
{
    /// <summary>
    /// Implementing this interface allows for greater control over singleton spawning per concrete class.
    /// </summary>
    public interface ISingletonBehavior
    {
        bool SetDontDestroyOnLoadWhenSpawning { get; }
        HideFlags HideFlagsToSetWhenSpawning { get; }
    }

    /// <summary>
    /// BASED ON: http://wiki.unity3d.com/index.php/Singleton
    /// Will by default set HideFlags.DontSave and DontDestroyOnLoad().
    /// These defaults can be overridden by implementing the ISingletonBehavior interface.
    /// 
    /// Original summary:
    /// Be aware this will not prevent a non singleton constructor such as `T myT = new T();`
    /// To prevent that, add `protected T () {}` to your singleton class.
    /// As a note, this is made as MonoBehaviour because we need Coroutines.
    /// </summary>
    [DisallowMultipleComponent]
    public abstract class SingletonBehavior<T> : MonoBehaviour where T : MonoBehaviour
    {
        // Analysis disable StaticFieldInGenericType
        static readonly bool DEBUG_ENABLED = false;
        static readonly HideFlags defaultHideFlags = HideFlags.DontSave;
        static readonly bool defaultSetDontDestroyOnLoadWhenSpawning = true;
        static T _instance;
        static object _lock = new object();
        static bool applicationIsQuitting = false;
        // Analysis restore StaticFieldInGenericType

        /// <summary>
        /// Gets the singleton instance, or creates one if none are present in the scene.
        /// Will return NULL when accessing it during application shutdown.
        /// </summary>
        /// <value>The instance.</value>
        public static T Instance
        {
            get
            {
                if (applicationIsQuitting) // Avoid creating new instances during shutdown, as these linger in the scene after play mode exits
                {
                    Debug.LogWarningFormat(
                        "[Singleton] Instance '{0}' already destroyed on application quit. Won't create again - returning null.",
                        typeof(T)
                    );
                    return null;
                }
                lock (_lock) // Make thread-safe
                {
                    if (_instance == null) // No instance created yet
                    {
                        var instances = FindObjectsOfType<T>(); // Find all
                        _instance = instances.Length > 0 ? instances[0] : null; // Grab first, if any
                        if (instances.Length > 1) // More than one instance - bad
                        {
                            Debug.LogErrorFormat(
                                "[Singleton] Multiple instances of singleton not allowed (type: {0})! Reopening the scene might fix it.",
                                typeof(T)
                            );
                            return _instance;
                        }
                        if (_instance == null) // We still dont have an instance after searching for it in the scene
                        {
                            CreateRuntimeSingleton();
                        }
                        else
                        {
                            if(DEBUG_ENABLED) Debug.LogFormat(_instance, "[Singleton] Using instance already created: {0}", _instance.gameObject.name);
                        }
                    }

                    return _instance;
                }
            }
        }

        static void CreateRuntimeSingleton()
        {
            Debug.Assert(_instance == null); // This function assumes that theres no current instance

            // Create new instance and track it
            GameObject singleton = new GameObject();
            _instance = singleton.AddComponent<T>();
            singleton.name = string.Format("+{0} (Singleton)", typeof(T).Name);

            // KGC - 141117 - Allowing more control over singleton instances from concrete class

            // Default values
            HideFlags tempHideFlags = defaultHideFlags;
            bool tempSetDontDestroyOnLoadWhenSpawning = defaultSetDontDestroyOnLoadWhenSpawning;

            // Grab case-specific values
            ISingletonBehavior iSingleton = _instance as ISingletonBehavior;
            if (iSingleton != null) // If concrete class implements the singleton interface
            {
                tempHideFlags = iSingleton.HideFlagsToSetWhenSpawning;
                tempSetDontDestroyOnLoadWhenSpawning = iSingleton.SetDontDestroyOnLoadWhenSpawning;
            }

            // Apply values
            singleton.hideFlags = tempHideFlags;
            if (tempSetDontDestroyOnLoadWhenSpawning)
            {
                DontDestroyOnLoad(singleton);
            }

            if(DEBUG_ENABLED) Debug.LogFormat(
                singleton,
                "[Singleton] An instance of {0} is needed in the scene, so '{1}' was created with DontDestroyOnLoad: {0} and HideFlags: {1}.",
                typeof(T),
                singleton,
                tempSetDontDestroyOnLoadWhenSpawning,
                tempHideFlags
            );
        }

        /// <summary>
        /// When Unity quits, it destroys objects in a random order.
        /// In principle, a Singleton is only destroyed when application quits.
        /// If any script calls Instance after it have been destroyed, 
        ///   it will create a buggy ghost object that will stay on the Editor scene
        ///   even after stopping playing the Application. Really bad!
        /// So, this was made to be sure we're not creating that buggy ghost object.
        /// </summary>
        public void OnDestroy()
        {
            applicationIsQuitting = true;
        }
    }
}
