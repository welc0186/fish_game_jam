using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alf.Utils
{

public class PersistentSingleton<T> : MonoBehaviour where T : Component {
    public bool AutoUnparentOnAwake = true;

    protected static T instance;
    protected bool _doomed;

    public static bool HasInstance => instance != null;
    public static T TryGetInstance() => HasInstance ? instance : null;

    public static T Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<T>();
                if (instance == null) {
                    var go = new GameObject(typeof(T).Name + " Auto-Generated");
                    instance = go.AddComponent<T>();
                }
            }

            return instance;
        }
    }

    /// <summary>
    /// Make sure to call base.Awake() in override if you need awake.
    /// awake will still be called even if gameObject will be destroyed (Destroy() is delayed to end of frame)
    /// Check _doomed to see if child is marked to be destroyed
    /// </summary>
    protected virtual void Awake() {
        InitializeSingleton();
    }

    protected virtual void InitializeSingleton() {
        if (!Application.isPlaying) return;

        if (AutoUnparentOnAwake) {
            transform.SetParent(null);
        }

        if (instance == null) {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        } else {
            if (instance != this) {
                _doomed = true;
                Destroy(gameObject);
            }
        }
    }
}
}