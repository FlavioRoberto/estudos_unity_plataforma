using System;
using System.Collections;
using Assembly_CSharp.Assets.Scripts.Enums;
using Assembly_CSharp.Assets.Scripts.Extensions;
using Assembly_CSharp.Assets.Scripts.Models;
using UnityEngine;
namespace Assembly_CSharp.Assets.Scripts.Components
{
    public class PlayerComponent : MonoBehaviour
    {
        public float AttackRadius = 1;
        private Player Player;
        public AudioClip JumpSound;
        public AudioClip HitSound;
        public LayerMask EnemyLayer;
        public Animator Animator;
        public Transform AttackPoint;
        private Rigidbody2D _rigidBody;
        private EMoveEagle _playerDirection;
        private AudioSource _audioSource;
        private static PlayerComponent Instance;

        void Awake()
        {
            Player = new Player();

            DontDestroyOnLoad(this);

            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            Player.DecreaseRecoverTime();
            OnDead();
            Attack();
            Move();
            Jump();
            Down();
        }
        void OnCollisionEnter2D(Collision2D colisor)
        {
            if (colisor.gameObject.layer == ((int)ELayer.GROUND))
                Player.SetInGround();

        }
        void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(AttackPoint.position, AttackRadius);
        }

        public void OnHit(float damage)
        {
            Action hitAction = () =>
            {
                _audioSource.PlayOneShot(HitSound);
                Animator.SetTrigger(ETrigger.HIT);
            };

            Action deadAction = () => Animator.SetTrigger(ETrigger.DEAD);

            Player.OnHit(damage, hitAction, deadAction);
        }

        private bool CanJump
        {
            get
            {
                return Input.GetButtonDown("Jump") && Player.CanJump;
            }
        }

        private void Move()
        {
            var movement = Input.GetAxis("Horizontal");
            _rigidBody.DefineVelocityInX(movement * Player.Speed);

            if (movement > 0 && Player.InGround && !Player.isAttacking)
            {
                SetMovePosition(EMoveEagle.RIGHT);
                return;
            }

            if (movement < 0 && Player.InGround && !Player.isAttacking)
            {
                SetMovePosition(EMoveEagle.LEFT);
                return;
            }

            if (movement == 0 && Player.InGround && !Player.isAttacking)
            {
                Player.StopMove();
                SetTransition(EPlayerTransition.IDLE);
            }

        }

        private void Down()
        {
            var inDown = _rigidBody.velocity.y < 0 && !Player.isMoving;

            if (inDown)
                SetTransition(EPlayerTransition.DOWN);
        }

        private void Attack()
        {
            if (!Input.GetButtonDown("Fire1"))
                return;

            Player.Attack();
            SetTransition(EPlayerTransition.SWORD_ATTACK);
            var hit = Physics2D.OverlapCircle(AttackPoint.position, AttackRadius, EnemyLayer);

            StartCoroutine(OnAttack());

            if (hit == null)
                return;

            hit.GetComponent<Enemy>().OnHit(Player.Damage, _playerDirection);
        }

        IEnumerator OnAttack()
        {
            yield return new WaitForSeconds(0.33f);
            Player.StopAttack();
        }

        private void Jump()
        {
            if (!CanJump)
                return;

            if (Player.CountJump == 0)
                SetTransition(EPlayerTransition.JUMP);
            else
                SetTransition(EPlayerTransition.DOUBLE_JUMP);

            _audioSource.PlayOneShot(JumpSound);
            Player.Jump();
            var velocity = _rigidBody.velocity;
            var impulse = Player.JumpForce + (velocity.y * -1);
            _rigidBody.AddForce(Vector2.up * impulse, ForceMode2D.Impulse);

        }

        private void SetMovePosition(EMoveEagle move)
        {
            Player.Move();
            _playerDirection = move;
            transform.eulerAngles = new Vector3(0, (int)move, 0);
            SetTransition(EPlayerTransition.RUN);
        }

        private void SetTransition(EPlayerTransition transition)
        {
            Animator.SetInteger("PlayerTransition", (int)transition);
        }

        private void OnDead()
        {
            if (Player.isDead)
                Destroy(gameObject, 1f);
        }

    }
}