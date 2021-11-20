

using Assembly_CSharp.Assets.Scripts.Enums;
using UnityEngine;

namespace Assembly_CSharp.Assets.Scripts.Components
{
    public class DoorBarrier : MonoBehaviour
    {
        private Animator _animator;

        void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void Open()
        {
            _animator.SetBool(ETrigger.BARRIER_DOWN, true);
        }

        public void Close()
        {
            _animator.SetBool(ETrigger.BARRIER_DOWN, false);
        }
    }
}