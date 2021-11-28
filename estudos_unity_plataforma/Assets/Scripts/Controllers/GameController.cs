using Assembly_CSharp.Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assembly_CSharp.Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }
        public GameObject GameOverPanel;
        public int Score { get; private set; }
        private int _currentLevel = 0;
        private Transform _checkpointPosition;

        public Text scoreText;

        private void Awake()
        {
            Instance = this;
            Time.timeScale = 1;
            RecoverScore();
        }

        public void GetCoin()
        {
            Score++;
            PrintScore();
            PlayerStorage.SaveCoin(Score);
        }

        public void GameOver()
        {
            Time.timeScale = 0;
            GameOverPanel.SetActive(true);
        }

        public void RestartLevel()
        {
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        public void SetCheckPoint(Transform checkpointPosition)
        {
            _checkpointPosition = checkpointPosition;
        }

        public void ResetCheckPoint()
        {
            var player = GameObject.FindGameObjectWithTag(ETag.PLAYER).transform;

            if (player != null)
                player.position = _checkpointPosition.position;
        }

        public void NextLevel()
        {
            _currentLevel++;
            PlayerStorage.SaveLevel(_currentLevel);
            SceneManager.LoadScene(_currentLevel);
        }

        private void RecoverScore()
        {
            Score = PlayerStorage.GetCoins();
            PrintScore();
        }

        private void PrintScore()
        {
            scoreText.text = $"x {Score}";
        }
    }
}