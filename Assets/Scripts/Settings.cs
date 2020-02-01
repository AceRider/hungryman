using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
 [SerializeField]
    private Slider livesSlider;

    [SerializeField]
    private Slider extraLifeSlider;

    [SerializeField]
    private Button startGameButton;

    [SerializeField]
    private GameObject[] lives;

    [SerializeField]
    private TMPro.TextMeshProUGUI extraLifePointsText;

    void OnEnable()
    {
        livesSlider.onValueChanged.AddListener(HandleLivesValueChanged);
        extraLifeSlider.onValueChanged.AddListener(HandleExtraLifeValueChanged);
        startGameButton.onClick.AddListener(LoadMainScene);
        DisplayLivesImages();
        GameState.extraLifePoints = 500;
        GameState.numberOfLives = 3;
        extraLifePointsText.text = GameState.extraLifePoints.ToString();
        extraLifeSlider.value = GameState.extraLifePoints / 500;
        GameState.score = 0;
    }

    void OnDisable()
    {
        livesSlider.onValueChanged.RemoveListener(HandleLivesValueChanged);
        extraLifeSlider.onValueChanged.RemoveListener(HandleExtraLifeValueChanged);
        startGameButton.onClick.RemoveListener(LoadMainScene);
    }

    void HandleLivesValueChanged(float value)
    {
        GameState.numberOfLives = (int)livesSlider.value;
        DisplayLivesImages();
    }

    void DisplayLivesImages()
    {
        for(int i = 0; i < lives.Length; i++)
        {
            lives[i].SetActive(false);
        }

        for(int i = 0; i < GameState.numberOfLives; i++)
        {
            lives[i].SetActive(true);
        }
    }

    void HandleExtraLifeValueChanged(float value)
    {
        GameState.extraLifePoints = (int)extraLifeSlider.value * 500;
        extraLifePointsText.text = GameState.extraLifePoints.ToString();
    }

    void LoadMainScene()
    {
        SceneManager.LoadScene(SceneUtils.Main);
    }
}
