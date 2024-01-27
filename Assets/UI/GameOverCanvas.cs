using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverCanvas : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Button restartButton;
    [SerializeField] private TextMeshProUGUI diverCountText;

    [SerializeField] private int diverCount = 6;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return null;
        GameManager.Instance.gameOverEvent.AddListener(EnableGameOverScreen);
        GameManager.Instance.diverExitEvent.AddListener(UpdateDiverCount);
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

    private void UpdateDiverCount()
    {
        diverCount -= 1;
        diverCountText.text = "Divers left: " + diverCount;
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
