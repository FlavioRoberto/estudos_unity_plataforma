using UnityEngine;
using UnityEngine.UI;

namespace Assembly_CSharp.Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }
        public int Score { get; private set; }

        public Text scoreText;

        private void Awake()
        {
            Instance = this;
        }

        public void GetCoin()
        {
            Score++;
            scoreText.text = $"x {Score}";
        }

    }
}