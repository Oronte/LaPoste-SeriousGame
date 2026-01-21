using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour, new()
{
    static T instance = null;
    public static T Instance => instance;

    protected virtual void Awake() => InitSingleton();

    /// <summary>
    /// Initialise l'instance du singleton si elle n'existe pas deja 
    /// </summary>
    void InitSingleton()
    {
        if (instance)
        {
            Destroy(this);
            return;
        }
        instance = this as T;
        name += $" [{instance.ToString()}]";
    }
}
