using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class LevelInfo
{
    public string name;
    public string scene;
}

public class Levels : ScriptableObject
{
    public LevelInfo[] levels;
}

public class LevelsListController : MonoBehaviour
{
    public Button _listItemRef;
    public Levels levelsInfo;

    void Start()
    {
        foreach (var levelInfo in levelsInfo.levels)
        {
            var item = Instantiate(_listItemRef, _listItemRef.transform.parent);
            item.GetComponentInChildren<TextMeshProUGUI>().text = levelInfo.name;
            item.onClick.AddListener(() =>
            {
                StartCoroutine(LoadLevel(levelInfo.scene));
            });
        }
        Destroy(_listItemRef.gameObject);
    }

    private IEnumerator LoadLevel(string name)
    {
        yield return SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        yield return SceneManager.UnloadSceneAsync("Menu");
    }
}
