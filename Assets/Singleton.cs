using UnityEngine;

public class Singleton<T> : MonoBehaviour
{
    private static T _instance;
    public static T Instance { get { return _instance; } set { if (_instance == null) { _instance = value; } else { Debug.LogWarning("more then one Instance for singleton"); } } }

    
}