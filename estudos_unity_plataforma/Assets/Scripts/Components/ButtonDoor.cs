using System;
using UnityEngine;

namespace Assembly_CSharp.Assets.Scripts.Components
{
    public class ButtonDoor : MonoBehaviour
    {
        private Animator _animator;
        private Action _buttonPressed;
        private Action _buttonUnpressed;
        private AudioSource _audioSource;

        void Start()
        {
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            _animator.SetBool("ButtonDoorPressed", true);

            _audioSource.Play();

            if (_buttonPressed != null)
                _buttonPressed();
        }

        void OnTriggerExit2D(Collider2D other)
        {
            _animator.SetBool("ButtonDoorPressed", false);

            if (_buttonUnpressed != null)
                _buttonUnpressed();
        }

        public void OnButtonPressed(Action action)
        {
            _buttonPressed = action;
        }

        public void OnButtonUnpressed(Action action)
        {
            _buttonUnpressed = action;
        }
    }
}