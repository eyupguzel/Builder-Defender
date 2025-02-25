using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance { get { return instance; } protected set => instance = value; } 

    protected void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            Init();
        }
        else
            Destroy(instance);
    }
    protected virtual void Init() { }

    protected void OnDestroy()
    {
        if(instance != null)
            Destroy(instance);
    }
}
