using UnityEngine;

public abstract class GenericSingleton<T>:MonoBehaviour where T: MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindOrCreateInstance();
            }
            return _instance;
        }
    }

    public static bool Ative
    {
        get => _instance != null;
    }

    private static T FindOrCreateInstance()
    {
        var Instance = GameObject.FindObjectOfType<T>();

        if (System.Object.ReferenceEquals(Instance, null))
        {
            Instance = new GameObject(typeof(T).Name).AddComponent<T>();
        }

        return Instance;
    }
}
