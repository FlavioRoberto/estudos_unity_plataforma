using System;
using System.Collections;
using Assembly_CSharp.Assets.Scripts.Enums;
using Assembly_CSharp.Assets.Scripts.Extensions;
using UnityEngine;

namespace Assembly_CSharp.Assets.Scripts.Components
{
    public class Goblin : Enemy
    {
        public float DistanceDetection = 1;
        public bool isRight = true;
        public float AtackDistance = 0;
        private bool isSeeingPlayer = false;
        private bool isAttacking = false;
        private bool gettingDamage = false;
        public Transform PointVision;
        public Transform Behind;
        private Animator _animator;
        private Rigidbody2D _rigidbody;
        private Vector2 _direction
        {
            get
            {
                return isRight ? Vector2.right : Vector2.left;
            }
        }

        private bool isStop { get { return !isSeeingPlayer || isAttacking || gettingDamage; } }

        protected void Start()
        {
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            DefineDirection();
            Move();
            SeePlayer();
        }

        void Move()
        {
            if (isStop)
            {
                if (!isAttacking)
                    SetTransition(EGoblinEnemyTransition.IDLE);

                _rigidbody.StopVelocity();
                return;
            }

            SetTransition(EGoblinEnemyTransition.WALK);

            if (isRight)
                _rigidbody.DefineVelocityInX(Speed);
            else
                _rigidbody.DefineVelocityInX(-Speed);
        }

        void DefineDirection()
        {
            if (isRight)
                transform.DefineDirectionRight();
            else
                transform.DefineDirectionLeft();
        }

        void SeePlayer()
        {
            var isSeeingPlayerInFront = SeePlayer(Physics2D.Raycast(PointVision.position, _direction, DistanceDetection));
            var isSeeingPlayerInBack = SeePlayer(Physics2D.Raycast(Behind.position, -_direction, DistanceDetection), () => isRight = !isRight);

            if (!isSeeingPlayerInBack && !isSeeingPlayerInFront)
            {
                isSeeingPlayer = false;
                isAttacking = false;
            }
        }

        bool SeePlayer(RaycastHit2D hit, Action onSeePlayer = null)
        {
            if (hit.collider == null)
                return false;

            if (hit.transform.CompareTag(ETag.PLAYER))
            {
                if (onSeePlayer != null)
                    onSeePlayer();

                OnSeeingPlayer(hit.transform);
                return true;
            }

            return false;
        }

        private void OnSeeingPlayer(Transform playTransform)
        {
            isSeeingPlayer = true;

            var playerDistance = Vector2.Distance(transform.position, playTransform.position);

            if (playerDistance <= AtackDistance)
                Attack(playTransform.GetComponent<Player>());
        }

        private void Attack(Player player)
        {
            isAttacking = true;
            SetTransition(EGoblinEnemyTransition.ATTACK);
            AttackPlayer(player);
        }

        //show gizmos only if object selected in scene
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawRay(PointVision.position, Vector2.right * DistanceDetection);
        }

        protected override void OnDead()
        {
            _animator.SetTrigger(ETrigger.DEAD);
            Speed = 0;
            Destroy(gameObject, 0.5f);
        }

        protected override void OnHitLeave(EMoveEagle direction)
        {
            gettingDamage = true;

            if (direction == EMoveEagle.RIGHT)
                _rigidbody.AddForce(Vector2.right * 800, ForceMode2D.Force);
            else
                _rigidbody.AddForce(Vector2.left * 800, ForceMode2D.Force);

            _animator.SetTrigger(ETrigger.HIT);
            StartCoroutine(CountTimeHit());
        }

        IEnumerator CountTimeHit()
        {
            yield return new WaitForSeconds(0.5f);
            gettingDamage = false;
        }

        private void SetTransition(EGoblinEnemyTransition transition)
        {
            _animator.SetInteger("Transition", (int)transition);
        }
    }
}