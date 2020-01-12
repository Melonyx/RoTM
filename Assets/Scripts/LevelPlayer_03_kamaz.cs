using System.Collections;
using UnityEngine;

public class LevelPlayer_03_kamaz : LevelPlayer
{
    public GameObject ball;
    public GameObject child;
    public GameObject dumper;

    private int? _answer = null;

    protected override void Prepare()
    {
        base.Prepare();
        dumper.transform.position = waypoints[4].position;
    }

    protected override IEnumerator Level_Coroutine()
    {
        StartCoroutine(MoveWithBouncing(ball.transform, 3f, waypoints[0].position, waypoints[1].position, 2.5f));
        child.transform.LookAt(waypoints[3].position);
        yield return WaitAndMove(2f, child.transform, 3f, waypoints[2].position, waypoints[3].position);

        ui.SetActive(true);
        // Wait for user's answer
        yield return new WaitUntil(() => _answer != null);
        ui.SetActive(false);

        if (_answer == 0)
        {
            StartCoroutine(Move(child.transform, 2.5f, waypoints[3].position, waypoints[6].position));
            yield return WaitAndMove(1f, dumper.transform, 2f, waypoints[4].position, waypoints[5].position);

            // Crash
            Handheld.Vibrate();
            resultUi.SetActive(true);
            resultText.text = "Вас сбил грузовик, будьте внимательны!\n<size=60%>Нельзя выбегать на проезжую часть!</size>";
        }
        else
        {
            StartCoroutine(WaitAndMove(1f, dumper.transform, 2f, waypoints[4].position, waypoints[7].position));
            yield return new WaitForSeconds(2.75f);
            yield return Explode(ball.transform, 1.75f, 0.35f);
            yield return new WaitForSeconds(1f);

            //Success
            resultUi.SetActive(true);
            resultText.text = "Молодец!\n<size=60%>Нельзя выбегать на проезжую часть!</size>";
        }
    }

    private IEnumerator Explode(Transform transform, float maxScale, float duration)
    {
        var startScale = transform.localScale;
        var targetScale = transform.localScale * maxScale;
        var t = 0f;
        while (t < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, t / duration);
            t += Time.deltaTime;
            yield return null;
        }
        var particleSystem = transform.GetChild(0).gameObject;
        particleSystem.SetActive(true);
        yield return null;
        transform.localScale = targetScale;
        transform.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(0.15f);
        transform.gameObject.SetActive(false);
    }

    public void SetAnswer(int answer)
    {
        _answer = answer;
    }
}
