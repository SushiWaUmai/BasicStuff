using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;

    /// <summary>
    /// Singleton
    /// </summary>
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject(nameof(T)).AddComponent<T>();
                DontDestroyOnLoad(instance.gameObject);
                return instance;
            }
            return instance;
        }
    }

    /// <summary>
    /// Defines wheter the Instance Exists in the Scene
    /// </summary>
    public static bool InstanceExists => instance != null;

    private void Awake()
    {
        if(instance == null)
        {
            Debug.LogError($"BasicSingleton on \"{gameObject.name}\", use \"Instance\" instead.", this);
        }
    }
}