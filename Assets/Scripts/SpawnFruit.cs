using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFruit : MonoBehaviour
{
    [SerializeField]
    private GameObject fruit;

    [SerializeField]
    private BoxCollider2D collider;

    [SerializeField]
    private SpriteRenderer fruitSpriteRenderer;

    [SerializeField]
    private List<Sprite> fruitSprites;

    void Start()
    {
        StartTimerToSpawnFruit();
    }

    IEnumerator StartFruitTimer()
    {
        int timer = 0;

        while(timer < 30)
        {
            timer++;
            yield return new WaitForSeconds(1);
        }

        fruitSpriteRenderer.enabled = true;
        collider.enabled = true;
    }

    public void StartTimerToSpawnFruit()
    {
        fruitSpriteRenderer.sprite = fruitSprites[GameState.level];
        fruitSpriteRenderer.enabled = false;
        StartCoroutine(StartFruitTimer());
    }
}
