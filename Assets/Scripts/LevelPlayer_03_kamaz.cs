using System.Collections;
using UnityEngine;

public class LevelPlayer_03_kamaz : LevelPlayer
{
    public GameObject ball;
    public GameObject child;

    protected override IEnumerator Level_Coroutine()
    {
        StartCoroutine(MoveWithBouncing(ball.transform, 3f, waypoints[0].position, waypoints[1].position, 2.5f));
        child.transform.LookAt(waypoints[3].position);
        yield return WaitAndMove(2f, child.transform, 3f, waypoints[2].position, waypoints[3].position);
    }
}
