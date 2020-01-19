using UnityEngine;
using UnityEngine.UI;

public class ResultUiController : MonoBehaviour
{
    [SerializeField] private Button _retry, _mainMenu;

    private void Start()
    {
        _retry.onClick.AddListener(() => KekSceneManager.ReloadCurrentLevel());
        _mainMenu.onClick.AddListener(() => KekSceneManager.LoadMenu());
    }
}
