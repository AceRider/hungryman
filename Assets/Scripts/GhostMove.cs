using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMove : MonoBehaviour
{
    [SerializeField]
    List<Transform> waypoints;

    int currentWaypoint = 0;

    [SerializeField]
    float speed = 0.005f;

    void Update()
    {
        if (gameObject.transform.position != waypoints[currentWaypoint].position)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, waypoints[currentWaypoint].position, speed * Time.deltaTime * 150);
        }
        else
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Count;
        }
    }

    public void ResetGhostMovement()
    {
        currentWaypoint = 0;
    }
}
