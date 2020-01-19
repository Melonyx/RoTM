using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsListController : MonoBehaviour
{
    public Button _listItemRef;
    public Levels levelsInfo;

    private Transform _parent;

    void Start()
    {
        _parent = _listItemRef.transform.parent;

        foreach (var levelInfo in levelsInfo.levels)
        {
            var item = Instantiate(_listItemRef, _parent);
            item.GetComponentInChildren<TextMeshProUGUI>().text = levelInfo.name;
            item.onClick.AddListener(() => OnButtonClicked(levelInfo));
        }
        Destroy(_listItemRef.gameObject);
    }

    private void OnButtonClicked(LevelInfo levelInfo)
    {
        StartCoroutine(LoadLevel(levelInfo.scene));
        _parent.gameObject.SetActive(false);
    }

    private IEnumerator LoadLevel(string name)
    {
        yield return SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        yield return SceneManager.UnloadSceneAsync("Menu");
    }
}
