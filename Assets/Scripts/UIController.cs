using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI scoreText;
    [SerializeField]
    private TMPro.TextMeshProUGUI highScoreText;
    [SerializeField]
    private TMPro.TextMeshProUGUI readyText;
    [SerializeField]
    private List<GameObject> levelIndicators;
    [SerializeField]
    private List<GameObject> lives;

    public void SetScoreText(int score)
    {
        scoreText.text = score.ToString();
    }
    public void SetHighScoreText(int score)
    {
        highScoreText.text = score.ToString();
    }
    public void ShowLevelIndicators()
    {
        for(int i = 1; i <= GameState.level; i++)
        {
            levelIndicators[i].SetActive(true);
        }
    }
    public void DisplayLivesImages()
    {
        for (int i = 0; i < lives.Count; i++)
        {
            lives[i].SetActive(false);
        }

        for (int i = 0; i < GameState.numberOfLives - 1; i++)
        {
            lives[i].SetActive(true);
        }
    }
    public void ToggleReadyText(bool shouldShow)
    {
        readyText.gameObject.SetActive(shouldShow);
    }
}
