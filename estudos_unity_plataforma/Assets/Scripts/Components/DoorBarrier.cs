

using Assembly_CSharp.Assets.Scripts.Enums;
using UnityEngine;

namespace Assembly_CSharp.Assets.Scripts.Components
{
    public class DoorBarrier : MonoBehaviour
    {
        private Animator _animator;
        private AudioSource _audioSource;

        void Start()
        {
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
            _audioSource.time = 9;
            _audioSource.Play();
        }

        public void Open()
        {
            _audioSource.Play();
            _animator.SetBool(ETrigger.BARRIER_DOWN, true);
        }

        public void Close()
        {
            _audioSource.Play();
            _animator.SetBool(ETrigger.BARRIER_DOWN, false);
        }
    }
}