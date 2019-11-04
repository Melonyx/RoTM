using System.Collections;
using UnityEngine;

public class LevelPlayer_02_trafficlight : LevelPlayer
{
    public GameObject lamba;
    public GameObject lada;
    public GameObject child;
    public MeshRenderer trafficLight;

    private int? _answer = null;

    protected override void Prepare()
    {
        base.Prepare();
        trafficLight.material.color = Color.red;
    }

    protected override IEnumerator Level_Coroutine()
    {
        //Lamba's race
        yield return Move(lamba.transform, 4f, waypoints[0].position, waypoints[1].position);
        yield return new WaitForSeconds(2f);
        StartCoroutine(ShowTrafficLight());
        ui.SetActive(true);
        // Wait for user's answer
        yield return new WaitUntil(() => _answer != null);
        ui.SetActive(false);
        // Lada's race
        if (trafficLight.material.color == Color.red)
        {
            StartCoroutine(WaitAndMove(1.5f, lada.transform, 1.5f, waypoints[4].position, waypoints[5].position));
            yield return Move(child.transform, 3f, waypoints[2].position, waypoints[8].position);
            Handheld.Vibrate();
            resultUi.SetActive(true);
            resultText.text = "Вас сбила машина, будьте внимательны!\n<size=60%>Дорогу нужно переходить на зелёный сигнал светофора!</size>";
        }
        else
        {
            StartCoroutine(Move(lada.transform, 1.5f, waypoints[4].position, waypoints[6].position));
            yield return Move(child.transform, 4f, waypoints[2].position, waypoints[3].position);
            yield return Move(lada.transform, 3.8f, waypoints[6].position, waypoints[7].position);
            resultUi.SetActive(true);
            resultText.text = "Молодец!\n<size=60%>Дорогу нужно переходить на зелёный сигнал светофора!</size>";
        }
    }

    private IEnumerator ShowTrafficLight()
    {
        yield return new WaitForSeconds(7f);
        trafficLight.material.color = Color.green;
    }

    public void SetAnswer(int answer)
    {
        _answer = answer;
    }
}
