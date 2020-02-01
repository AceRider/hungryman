using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Animator playerAnimator;

    private float direction = 0;
    public bool canIMove;
    Vector3 initialPosition;

    private void Start()
    {
        initialPosition = player.transform.position;
        ResetPlayer();
    }

    private void Update()
    {
        if (canIMove)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                player.transform.rotation = Quaternion.Euler(0, 0, 0);
                direction = 0;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                player.transform.rotation = Quaternion.Euler(0, 0, -90);
                direction = -90;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                player.transform.rotation = Quaternion.Euler(0, 0, 180);
                direction = 180;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                player.transform.rotation = Quaternion.Euler(0, 0, 90);
                direction = 90;
            }

            if (direction == 0)
            {
                transform.position += new Vector3(2 * Time.deltaTime, 0, 0);
            }
            else if (direction == -90f)
            {
                transform.position += new Vector3(0, -2 * Time.deltaTime, 0);
            }
            else if (direction == 180f)
            {
                transform.position += new Vector3(-2 * Time.deltaTime, 0, 0);
            }
            else
            {
                transform.position += new Vector3(0, 2 * Time.deltaTime, 0);
            }
        }
    }

    public void TogglePlayerAnimator(bool active)
    {
        playerAnimator.enabled = active;
    }

    public IEnumerator AddDelayBeforeGameStart()
    {
        canIMove = false;
        int timer = 0;
        while(timer < 1)
        {
            timer++;
            yield return new WaitForSeconds(1);
        }
        canIMove = true;
    }

    public void ResetPlayer()
    {
        player.transform.rotation = Quaternion.Euler(0, 0, 0);
        direction = 0;
        player.transform.position = initialPosition;
        StartCoroutine(AddDelayBeforeGameStart());
    }
}
