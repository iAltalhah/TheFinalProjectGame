using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrailPoint
{
    public Vector3 position;
    public float time;

    public TrailPoint(Vector3 position, float time)
    {
        this.position = position;
        this.time = time;
    }
}

public class RewindAnchor : MonoBehaviour
{
    [SerializeField] Transform player;

    [SerializeField] float delay = 4f;
    [SerializeField] float recordInterval = 0.05f;
    [SerializeField] float minDistanceToRecord = 0.05f;

    Queue<TrailPoint> trailPoints = new Queue<TrailPoint>();

    float recordTimer;
    Vector3 lastRecordedPosition;

    void Start()
    {
        if (player == null)
            return;

        transform.position = player.position;
        lastRecordedPosition = player.position;

        RecordPlayerPosition();
    }

    void LateUpdate()
    {
        if (player == null)
            return;

        RecordPlayerPath();
        FollowOldPlayerPath();
    }

    void RecordPlayerPath()
    {
        recordTimer += Time.deltaTime;

        if (recordTimer < recordInterval)
            return;

        recordTimer = 0f;

        float distanceFromLastPoint = Vector3.Distance(player.position, lastRecordedPosition);

        if (distanceFromLastPoint < minDistanceToRecord)
            return;

        RecordPlayerPosition();
    }

    void RecordPlayerPosition()
    {
        TrailPoint newPoint = new TrailPoint(player.position, Time.time);

        trailPoints.Enqueue(newPoint);

        lastRecordedPosition = player.position;
    }

    void FollowOldPlayerPath()
    {
        float targetTime = Time.time - delay;

        while (trailPoints.Count > 0 && trailPoints.Peek().time <= targetTime)
        {
            TrailPoint oldPoint = trailPoints.Dequeue();

            transform.position = oldPoint.position;
        }
    }
}