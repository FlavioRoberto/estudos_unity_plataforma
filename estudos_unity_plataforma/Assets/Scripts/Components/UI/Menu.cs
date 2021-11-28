using Assembly_CSharp.Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Components.UI
{
    public class Menu : MonoBehaviour
    {
        private int level;

        private void Awake()
        {
        }

        public void StartGame()
        {
            level = PlayerStorage.GetLevel();
            level = level == 0 ? 1 : level;
            PlayerStorage.SaveLevel(level);
            SceneManager.LoadScene(level);
        }
    }
}