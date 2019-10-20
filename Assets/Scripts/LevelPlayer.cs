using System.Collections;
using TMPro;
using UnityEngine;

public class LevelPlayer : MonoBehaviour
{
    public GameObject bus;
    public GameObject child;
    public GameObject ui;
    public GameObject badBus;
    public GameObject resultUi;
    public TextMeshProUGUI resultText;
    public Transform[] waypoints;

    private int? _answer = null;

    private void Start()
    {
        Prepare();
        StartCoroutine(Level_Coroutine());
    }

    private void Prepare()
    {
        child.SetActive(false);
        resultUi.SetActive(false);
        ui.SetActive(false);
    }
   
    private IEnumerator Level_Coroutine()
    {
        // Bus moving
        yield return Move(bus.transform, 5f, waypoints[0].position, waypoints[1].position);

        // Enable child
        yield return new WaitForSeconds(1f);
        child.SetActive(true);
        child.transform.LookAt(waypoints[3]);
        yield return Move(child.transform, 1.8f, waypoints[2].position, waypoints[3].position);

        // Enable UI
        yield return new WaitForSeconds(2f);
        ui.SetActive(true);
        
        // Wait for user's answer
        yield return new WaitUntil(() => _answer != null);
        ui.SetActive(false);

        if (_answer == 0)
        {
            // Left
            child.transform.LookAt(waypoints[4]);
            yield return Move(child.transform, 3f, waypoints[3].position, waypoints[4].position);
            StartCoroutine(Move(badBus.transform, 3f, waypoints[8].position, waypoints[9].position));
            StartCoroutine(WaitAndMove(1.2f, bus.transform, 5f, waypoints[1].position, waypoints[11].position));
            child.transform.LookAt(waypoints[5]);
            yield return Move(child.transform, 2.95f, waypoints[4].position, waypoints[5].position);
            // Crash
            Handheld.Vibrate();
            resultUi.SetActive(true);
            resultText.text = "Вас сбила машина, будьте внимательны!\n<size=60%>Автобус всегда нужно обходить спереди!</size>";
        }
        else if (_answer == 1)
        {
            // Right
            child.transform.LookAt(waypoints[6]);
            yield return Move(child.transform, 2f, waypoints[3].position, waypoints[6].position);
            StartCoroutine(Move(badBus.transform, 2f, waypoints[8].position, waypoints[10].position));
            child.transform.LookAt(waypoints[7]);
            yield return Move(child.transform, 5.5f, waypoints[6].position, waypoints[7].position);
            StartCoroutine(WaitAndMove(1f, badBus.transform, 6f, waypoints[10].position, waypoints[12].position));
            yield return Move(bus.transform, 5f, waypoints[1].position, waypoints[11].position);
            resultUi.SetActive(true);
            resultText.text = "Молодец!\n<size=60%>Автобус всегда нужно обходить спереди!</size>";
        }

        yield return null;
    }

    public void SetAnswer(int answer)
    {
        _answer = answer;
    }

    private IEnumerator WaitAndMove(float waitingTime, Transform transform, float movingTime, Vector3 start, Vector3 stop)
    {
        yield return new WaitForSeconds(waitingTime);
        yield return Move(transform, movingTime, start, stop);
    }

    private IEnumerator Move(Transform transform, float movingTime, Vector3 start, Vector3 stop)
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
}
