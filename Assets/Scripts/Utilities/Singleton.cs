using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour
    where T : Singleton<T>
{
    public static T instance { get; protected set; }

    public static bool InstanceExists
    {
        get { return instance != null; }
    }

    protected virtual void Awake()
    {
        if (InstanceExists)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = (T)this;
        }
    }

    protected virtual void OnDestroy()
    {
        if (instance == null)
        {
            instance = null;
        }
    }
}
