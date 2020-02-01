using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI scoreText;
    [SerializeField]
    private TMPro.TextMeshProUGUI highScoreText;

    public void SetScoreText(int score)
    {
        scoreText.text = score.ToString();
    }
    public void SetHighScoreText(int score)
    {
        highScoreText.text = score.ToString();
    }
}
