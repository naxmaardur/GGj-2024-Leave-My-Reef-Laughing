using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        if(Instance != this)
        {
            selfDestruct();
        }
    }
    private void selfDestruct()
    {
        Destroy(this.gameObject);
    }


    public Coroutine RunCoroutine(IEnumerator enumerator)
    {
        return StartCoroutine(enumerator);
    }

    public void StopACoroutine(Coroutine coroutine)
    {
        StopCoroutine(coroutine);
    }

}
