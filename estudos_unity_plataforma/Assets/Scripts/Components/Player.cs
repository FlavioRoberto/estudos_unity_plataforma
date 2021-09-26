using Assembly_CSharp.Assets.Scripts.Enums;
using Assembly_CSharp.Assets.Scripts.Extensions;
using UnityEngine;

namespace Assembly_CSharp.Assets.Scripts.Components
{
    public class Player : MonoBehaviour
    {
        public float Speed = 1;
        public float JumpForce = 1;
        public Animator Animator;
        private Rigidbody2D _rigidBody;
        private int _countJump = 0;

        void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            Move();
            Jump();
            Down();
        }

        void OnCollisionEnter2D(Collision2D colisor)
        {
            if (colisor.gameObject.layer == ((int)ELayer.GROUND))
                _countJump = 0;
        }

        private bool CanJump
        {
            get
            {
                return Input.GetButtonDown("Jump") && _countJump < 2;
            }
        }

        private bool InGround
        {
            get
            {
                return _countJump == 0;
            }
        }

        private void Move()
        {
            var movement = Input.GetAxis("Horizontal");
            _rigidBody.velocity = new Vector2(movement * Speed, _rigidBody.velocity.y);

            if (movement > 0 && InGround)
            {
                Animator.SetMovePosition(EMoveEagle.RIGHT);
                return;
            }

            if (movement < 0 && InGround)
            {
                Animator.SetMovePosition(EMoveEagle.LEFT);
                return;
            }

            if (InGround)
                Animator.SetTransition(EPlayerTransition.IDLE);

        }

        private void Down()
        {
            var inDown =  _rigidBody.velocity.y < 0;
            
            if (inDown)
                Animator.SetTransition(EPlayerTransition.DOWN);
        }

        private void Jump()
        {
            if (!CanJump)
                return;

            if (_countJump == 0)
                Animator.SetTransition(EPlayerTransition.JUMP);
            else
                Animator.SetTransition(EPlayerTransition.DOUBLE_JUMP);

            _countJump += 1;
            var velocity = _rigidBody.velocity;
            var impulse = JumpForce + (velocity.y * -1);
            _rigidBody.AddForce(Vector2.up * impulse, ForceMode2D.Impulse);

        }

    }
}
