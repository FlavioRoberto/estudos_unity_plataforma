using Assembly_CSharp.Assets.Scripts.Enums;
using UnityEngine;

namespace Assembly_CSharp.Assets.Scripts.Components
{
    public class Player : MonoBehaviour
    {
        public const string PLAYER_TRANSITION = "PlayerTransition";
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

            if (movement > 0)
            {
                SetMovePosition(0);
                return;
            }

            if (movement < 0)
            {
                SetMovePosition(180);
                return;
            }

            if (InGround)
                Animator.SetInteger(PLAYER_TRANSITION, (int)EAnimatorTransition.IDLE);
        }

        private void SetMovePosition(int eagles)
        {
            Animator.SetInteger(PLAYER_TRANSITION, (int)EAnimatorTransition.RUN);
            transform.eulerAngles = new Vector3(0, eagles, 0);
        }

        void OnCollisionEnter2D(Collision2D colisor)
        {
            if (colisor.gameObject.layer == ((int)ELayer.GROUND))
                _countJump = 0;
        }

        private void Jump()
        {
            if (CanJump)
            {
                Animator.SetInteger(PLAYER_TRANSITION, (int)EAnimatorTransition.JUMP);
                _countJump += 1;
                var velocity = _rigidBody.velocity;
                var impulse = JumpForce + (velocity.y * -1);
                _rigidBody.AddForce(Vector2.up * impulse, ForceMode2D.Impulse);
            }
        }
    }
}
