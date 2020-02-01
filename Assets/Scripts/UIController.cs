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
    private List<GameObject> levelIndicators;

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
}
