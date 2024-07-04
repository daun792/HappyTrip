using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMovingLift : MonoBehaviour
{
    [SerializeField] float moveTime = 3f;
    [SerializeField] Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        MoveToNextPoint();
    }

    private void MoveToNextPoint()
    {
        transform.DOMove(waypoints[currentWaypointIndex].position, moveTime).
            OnComplete(() => MoveToNextPoint());
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }
}
