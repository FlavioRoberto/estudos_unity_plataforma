

using Assembly_CSharp.Assets.Scripts.Enums;
using UnityEngine;

namespace Assembly_CSharp.Assets.Scripts.Components
{
    public class Coin : MonoBehaviour
    {
        private Animator _animator;

        void Start()
        {
            _animator = GetComponent<Animator>();
        }

        void OnTriggerEnter2D(Collider2D colisor)
        {
            if (colisor.gameObject.CompareTag(ETag.PLAYER))
            {
                GameController.Instance.GetCoin();
                _animator.SetTrigger(ETrigger.GET_COIN);
                Destroy(gameObject, 0.5f);
            }
        }
    }
}