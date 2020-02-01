using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject bottomRightPortal;
    [SerializeField]
    private GameObject bottomLeftPortal;
    [SerializeField]
    private GameObject topRightPortal;
    [SerializeField]
    private GameObject topLeftPortal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bottomLeftPortal")
        {
            player.transform.position = new Vector3(bottomRightPortal.transform.position.x - 1f, bottomRightPortal.transform.position.y, 0);
        }
        else if (collision.gameObject.tag == "topLeftPortal")
        {
            player.transform.position = new Vector3(topRightPortal.transform.position.x - 1f, topRightPortal.transform.position.y, 0);
        }
        else if (collision.gameObject.tag == "bottomRightPortal")
        {
            player.transform.position = new Vector3(bottomLeftPortal.transform.position.x + 1f, bottomLeftPortal.transform.position.y, 0);
        }
        else if (collision.gameObject.tag == "topRightPortal")
        {
            player.transform.position = new Vector3(topLeftPortal.transform.position.x + 1f, topLeftPortal.transform.position.y, 0);
        }
    }

}
