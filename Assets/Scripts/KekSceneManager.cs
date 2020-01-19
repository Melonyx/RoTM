using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KekSceneManager : MonoBehaviour
{
    private const string MENU_SCENE_NAME = "Menu";
    private const string MAIN_SCENE_NAME = "Main";

    private static string _currentLevel;

    private static KekSceneManager _instance;
    private static KekSceneManager instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<KekSceneManager>();
            return _instance;
        }
    }

    public static void ReloadCurrentLevel() => instance.StartCoroutine(ReloadCurrentLevel_Coroutine());
    public static void LoadLevel(string name) => instance.StartCoroutine(LoadLevel_Coroutine(name));
    public static void LoadMenu() => instance.StartCoroutine(LoadMenu_Coroutine());

    private static IEnumerator ReloadCurrentLevel_Coroutine()
    {
        yield return LoadLevel_Coroutine(_currentLevel);
    }

    private static IEnumerator LoadLevel_Coroutine(string name)
    {
        yield return UnloadAllLevels();
        _currentLevel = name;
        yield return SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        if (SceneManager.GetSceneByName(MENU_SCENE_NAME).IsValid())
            yield return SceneManager.UnloadSceneAsync(MENU_SCENE_NAME);
    }

    private static IEnumerator LoadMenu_Coroutine()
    {
        yield return UnloadAllLevels();
        yield return SceneManager.LoadSceneAsync(MENU_SCENE_NAME, LoadSceneMode.Additive);
    }

    private static IEnumerator UnloadAllLevels()
    {
        for (var i = 0; i < SceneManager.sceneCount; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            if (scene.name != MAIN_SCENE_NAME && scene.name != MENU_SCENE_NAME)
                yield return SceneManager.UnloadSceneAsync(scene);
            yield return null;
        }
    }
}
