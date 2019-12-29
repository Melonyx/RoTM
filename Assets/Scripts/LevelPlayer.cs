using System.Collections;
using TMPro;
using UnityEngine;

public abstract class LevelPlayer : MonoBehaviour
{
    public GameObject ui;
    public GameObject resultUi;
    public TextMeshProUGUI resultText;
    public Transform[] waypoints;

    protected virtual void Prepare()
    {
        resultUi.SetActive(false);
        ui.SetActive(false);
    }

    protected abstract IEnumerator Level_Coroutine();

    protected IEnumerator WaitAndMove(float waitingTime, Transform transform, float movingTime, Vector3 start, Vector3 stop)
    {
        yield return new WaitForSeconds(waitingTime);
        yield return Move(transform, movingTime, start, stop);
    }

    protected IEnumerator Move(Transform transform, float movingTime, Vector3 start, Vector3 stop)
    {
        var time = 0f;
        while (time < movingTime)
        {
            transform.position = Vector3.Lerp(start, stop, time / movingTime);
            yield return null;
            time += Time.deltaTime;
        }
        transform.position = stop;
    }

    protected IEnumerator MoveWithBouncing(Transform transform, float movingTime, Vector3 start, Vector3 stop, float period)
    {
        var amp = start.y - stop.y;
        var time = 0f;

        while (time < movingTime)
        {
            var position = Vector3.Lerp(start, stop, time / movingTime);
            position.y = stop.y + amp * Mathf.Abs(Mathf.Cos(period*time)) * (movingTime - time) / movingTime;
            transform.position = position;
            yield return null;
            time += Time.deltaTime;
        }
        transform.position = stop;
    }

    private void Start()
    {
        Prepare();
        StartCoroutine(Level_Coroutine());
    }
}
