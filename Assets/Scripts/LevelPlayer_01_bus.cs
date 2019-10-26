using System.Collections;
using UnityEngine;

public class LevelPlayer_01_bus : LevelPlayer
{
    public GameObject bus;
    public GameObject child;
    public GameObject badBus;

    private int? _answer = null;

    protected override void Prepare()
    {
        base.Prepare();
        child.SetActive(false);
    }
   
    protected override IEnumerator Level_Coroutine()
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
}
