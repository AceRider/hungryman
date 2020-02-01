using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject dots;

    [SerializeField]
    private GameObject powerUps;

    [SerializeField]
    private UIController uiController;

    private void OnEnable()
    {
        IncreaseScore(true);
        GotPowerUp(true);
        GameState.highScore = PlayerPrefs.GetInt(SceneUtils.HighScore, 0);
        uiController.SetHighScoreText(GameState.highScore);
    }

    private void OnDisable()
    {
        IncreaseScore(false);
        GotPowerUp(false);
    }

    private void IncreaseScore(bool isIncrease)
    {
        if (dots != null)
        {
            foreach (Transform dot in dots.transform.GetComponentInChildren<Transform>())
            {
                Dot dotScript = dot.gameObject.GetComponent<Dot>();
                if (dotScript != null)
                {
                    if (isIncrease)
                        dotScript.IncreaseScore += HandleIncreaseScore;
                    else
                        dotScript.IncreaseScore -= HandleIncreaseScore;
                }
            }
        }
    }

    private void GotPowerUp(bool gotPowerUp)
    {
        if (powerUps != null)
        {
            foreach (Transform powerUp in powerUps.transform.GetComponentInChildren<Transform>())
            {
                PowerUp powerUpScript = powerUp.gameObject.GetComponent<PowerUp>();
                if (powerUpScript != null)
                {
                    if (gotPowerUp)
                        powerUpScript.IncreaseScore += HandleIncreaseScore;
                    else
                        powerUpScript.IncreaseScore -= HandleIncreaseScore;
                }
            }
        }
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt(SceneUtils.HighScore, GameState.highScore);
    }

    void HandleIncreaseScore(int amount)
    {
        if(GameState.highScore <= GameState.score)
        {
            GameState.highScore = GameState.score;
            GameState.highScore += amount;
            uiController.SetHighScoreText(GameState.highScore);
        }
        GameState.score += amount;
        uiController.SetScoreText(GameState.score);
    }
}
