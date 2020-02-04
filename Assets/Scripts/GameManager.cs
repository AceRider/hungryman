using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UIController uiController;
    [SerializeField]
    private GameObject dots;
    [SerializeField]
    private GameObject powerUps;
    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private Animator playerAnimator;
    [SerializeField]
    private List<GameObject> mazes;   
    [SerializeField]
    private List<GameObject> dotsList;
    [SerializeField]
    private List<GameObject> ghosts;
  

    private void OnEnable()
    {
        GameState.dotCount = 130;
        GameState.powerUpCount = 4;
        uiController.DisplayLivesImages();
        StartCoroutine(HandleMoveGhosts());
        IncreaseScore(true);
        GotPowerUp(true);
        GhostCollider(true);
        GameState.highScore = PlayerPrefs.GetInt(SceneUtils.HIGHSCORE_STR, 0);
        uiController.SetHighScoreText(GameState.highScore);
    }

    private void OnDisable()
    {
        IncreaseScore(false);
        GotPowerUp(false);
        GhostCollider(false);
    }

    private void GhostCollider(bool isCollider)
    {
        foreach (GameObject ghost in ghosts)
        {
            Ghost ghostScript = ghost.GetComponent<Ghost>();
            if (ghostScript != null)
            {
                if (isCollider)
                {
                    ghostScript.GhostCollidedWithPlayer += HandleGhostCollidedWithPlayer;
                    //ghostScript.IncreaseScore += HandleIncreaseScore;
                }
                else
                {
                    ghostScript.GhostCollidedWithPlayer -= HandleGhostCollidedWithPlayer;
                    //ghostScript.IncreaseScore -= HandleIncreaseScore;
                }
            }
        }
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
                    {
                        dotScript.IncreaseScore += HandleIncreaseScore;
                        dotScript.RemoveDot += HandleRemoveDot;
                    }
                    else
                    {
                        dotScript.IncreaseScore -= HandleIncreaseScore;
                        dotScript.RemoveDot -= HandleRemoveDot;
                    }
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
        PlayerPrefs.SetInt(SceneUtils.HIGHSCORE_STR, GameState.highScore);
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

    void HandleRemoveDot()
    {
        GameState.dotCount--;
        CheckIfBoardHasBeenCleared();
    }

    void CheckIfBoardHasBeenCleared()
    {
        if(GameState.dotCount == 0 && GameState.powerUpCount == 0)
        {
            StartNewBoard();
        }
    }

    void StartNewBoard()
    {
        if(GameState.level < mazes.Count - 1)
        {
            mazes[GameState.level].SetActive(false);
            GameState.level++;
            mazes[GameState.level].SetActive(true);
            GameState.dotCount = 130;
            RestoreDotsForNewBoard();
            GameState.powerUpCount = 4;
            playerMovement.ResetPlayer();
            uiController.ShowLevelIndicators();
            StartCoroutine(playerMovement.AddDelayBeforeGameStart());
            StartCoroutine(HandleMoveGhosts());
        }
    }

    void RestoreDotsForNewBoard()
    {
        foreach(GameObject dotsObject in dotsList)
        {
            dotsObject.SetActive(false);
        }

        dotsList[GameState.level].SetActive(true);

        foreach (Transform dot in dotsList[GameState.level].transform.GetComponentInChildren<Transform>())
        {
            Dot dotScript = dot.gameObject.GetComponent<Dot>();
            if(dotScript)
            {
                dotScript.RemoveDot += HandleRemoveDot;
                dotScript.IncreaseScore += HandleIncreaseScore;
            }
        }

        for(int i = 0; i < powerUps.transform.childCount; i++)
        {
            SpriteRenderer renderer = powerUps.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>();
            if(renderer != null)
            {
                renderer.enabled = true;
            }

            BoxCollider2D col2D = powerUps.transform.GetChild(i).gameObject.GetComponent<BoxCollider2D>();
            if(col2D != null)
            {
                col2D.enabled = true;
            }
        }
    }

    void CheckIfPlayerEarnedExtraLife()
    {
        if(GameState.score >= GameState.extraLifePoints && !GameState.hasExtraLifeBeenGiven)
        {
            GameState.hasExtraLifeBeenGiven = true;
            GameState.numberOfLives++;
            uiController.DisplayLivesImages();
        }
    }

    IEnumerator HandleMoveGhosts()
    {
        int timer = 0;

        while (timer < 10)
        {
            if (timer > 8)
            {     
                ghosts[3].GetComponent<GhostMove>().enabled = true;
            }
            else if (timer > 6)
            {
                ghosts[2].GetComponent<GhostMove>().enabled = true;
            }
            else if (timer > 4)
            {
                ghosts[1].GetComponent<GhostMove>().enabled = true;
            }
            else
            {
                ghosts[0].GetComponent<GhostMove>().enabled = true;
            }
            timer++;
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator HandlePlayerFaintAftermath()
    {
        int timer = 0;

        while (timer < 2)
        {
            timer++;
            yield return new WaitForSeconds(1);
        }

        GameState.numberOfLives--;

        if (GameState.numberOfLives > 0)
        {
            uiController.DisplayLivesImages();
            playerAnimator.SetBool("faint", false);
            playerMovement.ResetPlayer();
            GhostsBackToBox();
        }
        else
        {
            SceneManager.LoadScene(SceneUtils.Start);
        }
    }

    void HandleGhostCollidedWithPlayer()
    {
        playerAnimator.SetBool("faint", true);
        playerMovement.canIMove = false;

        foreach (GameObject ghost in ghosts)
        {
            ghost.SetActive(false);
        }

        StartCoroutine(HandlePlayerFaintAftermath());
    }

    void GhostsBackToBox()
    {
        foreach (GameObject ghost in ghosts)
        {
            ghost.SetActive(true);
            GhostMove ghostMoveScript = ghost.GetComponent<GhostMove>();

            if (ghostMoveScript != null)
            {
                ghostMoveScript.enabled = false;
            }

            Ghost ghostScript = ghost.GetComponent<Ghost>();
            if (ghostScript != null)
            {
                ghostScript.ResetGhost();
            }
        }

        StartCoroutine(HandleMoveGhosts());
    }
}
