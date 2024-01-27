using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public UnityEvent gameOverEvent;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        if(Instance != this)
        {
            selfDestruct();
        }
        DontDestroyOnLoad(gameObject);
    }
    private void selfDestruct()
    {
        Destroy(this.gameObject);
    }

    [Button]
    public void GameOver()
    {
        Debug.Log("Game Over");
        gameOverEvent?.Invoke();
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
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
