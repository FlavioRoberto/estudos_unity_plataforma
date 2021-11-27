

using Assembly_CSharp.Assets.Scripts.Enums;
using UnityEngine;

namespace Assembly_CSharp.Assets.Scripts.Components
{
    public class Coin : MonoBehaviour
    {
        public AudioClip GetCoinSound;
        private Animator _animator;
        private AudioSource _audioSource;

        void Start()
        {
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
        }

        void OnTriggerEnter2D(Collider2D colisor)
        {
            if (colisor.gameObject.CompareTag(ETag.PLAYER))
            {
                GameController.Instance.GetCoin();
                _audioSource.PlayOneShot(GetCoinSound);
                _animator.SetTrigger(ETrigger.GET_COIN);
                Destroy(gameObject, 0.5f);
            }
        }
    }
}