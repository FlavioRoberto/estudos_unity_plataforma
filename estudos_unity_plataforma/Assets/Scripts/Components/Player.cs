using System.Collections;
using Assembly_CSharp.Assets.Scripts.Enums;
using UnityEngine;
namespace Assembly_CSharp.Assets.Scripts.Components
{
    public class Player : MonoBehaviour
    {
        public float Health = 3;
        public float Speed = 1;
        public float JumpForce = 1;
        public float AttackRadius;
        public float Damage = 1;
        private int _countJump = 0;
        private bool isAttacking = false;
        private bool isMoving = false;
        public Animator Animator;
        public Transform AttackPoint;
        private Rigidbody2D _rigidBody;
        private EMoveEagle _playerDirection;
        public LayerMask EnemyLayer;

        void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            Attack();
            Move();
            Jump();
            Down();
        }
        void OnCollisionEnter2D(Collision2D colisor)
        {
            if (colisor.gameObject.layer == ((int)ELayer.GROUND))
                _countJump = 0;
                
        }
        void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(AttackPoint.position, AttackRadius);
        }

        public void OnHit(float damage)
        {
            Animator.SetTrigger(ETrigger.HIT);
            Health -= 1;

            if (Health <= 0)
                OnDead();
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

            if (movement > 0 && InGround && !isAttacking)
            {
                SetMovePosition(EMoveEagle.RIGHT);
                return;
            }

            if (movement < 0 && InGround && !isAttacking)
            {
                SetMovePosition(EMoveEagle.LEFT);
                return;
            }

            if (movement == 0 && InGround && !isAttacking)
            {
                isMoving = false;
                SetTransition(EPlayerTransition.IDLE);
            }

        }

        private void Down()
        {
            var inDown = _rigidBody.velocity.y < 0 && !isMoving;

            if (inDown)
                SetTransition(EPlayerTransition.DOWN);
        }

        private void Attack()
        {
            if (!Input.GetButtonDown("Fire1"))
                return;

            isAttacking = true;
            SetTransition(EPlayerTransition.SWORD_ATTACK);
            var hit = Physics2D.OverlapCircle(AttackPoint.position, AttackRadius, EnemyLayer);

            StartCoroutine(OnAttack());

            if (hit == null)
                return;

            hit.GetComponent<Enemy>().OnHit(Damage, _playerDirection);
        }

        IEnumerator OnAttack()
        {
            yield return new WaitForSeconds(0.33f);
            isAttacking = false;
        }

        private void OnDead()
        {
            Speed = 0;
            Animator.SetTrigger(ETrigger.DEAD);
        }

        private void Jump()
        {
            if (!CanJump)
                return;

            if (_countJump == 0)
                SetTransition(EPlayerTransition.JUMP);
            else
                SetTransition(EPlayerTransition.DOUBLE_JUMP);

            _countJump += 1;
            var velocity = _rigidBody.velocity;
            var impulse = JumpForce + (velocity.y * -1);
            _rigidBody.AddForce(Vector2.up * impulse, ForceMode2D.Impulse);

        }

        private void SetMovePosition(EMoveEagle move)
        {
            isMoving = true;
            _playerDirection = move;
            transform.eulerAngles = new Vector3(0, (int)move, 0);
            SetTransition(EPlayerTransition.RUN);
        }

        private void SetTransition(EPlayerTransition transition)
        {
            Animator.SetInteger("PlayerTransition", (int)transition);
        }

    }
}
