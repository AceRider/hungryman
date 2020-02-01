using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Eatable : MonoBehaviour
{
    public event Action<int> IncreaseScore;
    protected int points;

    [SerializeField]
    TMPro.TextMeshProUGUI pointsText;

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    private void EnableObject(bool isEnable)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = isEnable;
        }
        if (boxCollider != null)
        {
            boxCollider.enabled = isEnable;
        }
    }

    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        EnableObject(true);
    }

    protected void HandleObjectWasEaten()
    {
        EnableObject(false);

        if(pointsText != null)
        {
            pointsText.gameObject.SetActive(true);
            pointsText.text = points.ToString();
        }
        StartCoroutine(StartTimerToHideText());
        IncreaseScore?.Invoke(points);
    }

    IEnumerator StartTimerToHideText()
    {
        int timer = 0;
        while(timer < 1)
        {
            timer++;
            yield return new WaitForSeconds(1);
        }
        if(pointsText != null)
            pointsText.gameObject.SetActive(false);
    }
}
