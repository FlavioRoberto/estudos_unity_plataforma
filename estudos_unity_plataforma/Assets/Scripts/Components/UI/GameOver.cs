using Assembly_CSharp.Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Button Button;

    void Start()
    {
        Button.onClick.AddListener(Restart);
    }

    private void Restart()
    {
        GameController.Instance.RestartLevel();
    }
}