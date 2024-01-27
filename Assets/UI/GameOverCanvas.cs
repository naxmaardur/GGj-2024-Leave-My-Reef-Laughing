using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverCanvas : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Button restartButton;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return null;
        GameManager.Instance.gameOverEvent.AddListener(EnableGameOverScreen);
        restartButton.onClick.AddListener(Restart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EnableGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }

    private void Restart()
    {
        GameManager.Instance.RestartGame();
    }

    private void OnDestroy()
    {
        GameManager.Instance.gameOverEvent.RemoveListener(EnableGameOverScreen);
    }
}
