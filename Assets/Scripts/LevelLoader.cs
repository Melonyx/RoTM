using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }
}
