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
        public AudioClip JumpSound;
        public AudioClip HitSound;
        public AudioClip RunSound;
        public LayerMask EnemyLayer;
        public Animator Animator;
        public Transform AttackPoint;
        private Player _player;
        private Rigidbody2D _rigidBody;
        private EMoveEagle _playerDirection;
        private AudioSource _audioSource;
        private static PlayerComponent Instance;

        void Awake()
        {
            DontDestroyOnLoad(this);

            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        void Start()
        {
            _player = GetComponent<Player>();
            _rigidBody = GetComponent<Rigidbody2D>();
            _audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            _player.DecreaseRecoverTime();
            OnDead();
            Attack();
            Move();
            Jump();
            Down();
        }
        void OnCollisionEnter2D(Collision2D colisor)
        {
            if (colisor.gameObject.layer == ((int)ELayer.GROUND))
                _player.SetInGround();

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

            _player.OnHit(damage, hitAction, deadAction);
        }

        private bool CanJump
        {
            get
            {
                return Input.GetButtonDown("Jump") && _player.CanJump;
            }
        }

        private void Move()
        {
            var movement = Input.GetAxis("Horizontal");
            _rigidBody.DefineVelocityInX(movement * _player.Speed);

            if (movement > 0 && _player.InGround && !_player.isAttacking)
            {
                SetMovePosition(EMoveEagle.RIGHT);
                return;
            }

            if (movement < 0 && _player.InGround && !_player.isAttacking)
            {
                SetMovePosition(EMoveEagle.LEFT);
                return;
            }

            if (movement == 0 && _player.InGround && !_player.isAttacking)
            {
                _player.StopMove();
                SetTransition(EPlayerTransition.IDLE);
            }

        }

        private void Down()
        {
            var inDown = _rigidBody.velocity.y < 0 && !_player.isMoving;

            if (inDown)
                SetTransition(EPlayerTransition.DOWN);
        }

        private void Attack()
        {
            if (!Input.GetButtonDown("Fire1"))
                return;

            _player.Attack();
            SetTransition(EPlayerTransition.SWORD_ATTACK);
            var hit = Physics2D.OverlapCircle(AttackPoint.position, AttackRadius, EnemyLayer);

            StartCoroutine(OnAttack());

            if (hit == null)
                return;

            hit.GetComponent<Enemy>().OnHit(_player.Damage, _playerDirection);
        }

        IEnumerator OnAttack()
        {
            yield return new WaitForSeconds(0.33f);
            _player.StopAttack();
        }

        private void Jump()
        {
            if (!CanJump)
                return;

            if (_player.CountJump == 0)
                SetTransition(EPlayerTransition.JUMP);
            else
                SetTransition(EPlayerTransition.DOUBLE_JUMP);

            _audioSource.PlayOneShot(JumpSound);
            _player.Jump();
            var velocity = _rigidBody.velocity;
            var impulse = _player.JumpForce + (velocity.y * -1);
            _rigidBody.AddForce(Vector2.up * impulse, ForceMode2D.Impulse);

        }

        private void SetMovePosition(EMoveEagle move)
        {
            _player.Move();

             if (!_audioSource.isPlaying)
                _audioSource.PlayOneShot(RunSound);

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
            if (_player.isDead)
                Destroy(gameObject, 1f);
        }

    }
}