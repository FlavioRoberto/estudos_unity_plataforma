using Assembly_CSharp.Assets.Scripts.Enums;
using Assembly_CSharp.Assets.Scripts.Extensions;
using UnityEngine;

namespace Assembly_CSharp.Assets.Scripts.Components
{
    public class Goblin : Enemy
    {
        public float DistanceDetection = 1;
        public bool isRight = true;
        public Transform PointVision;
        public float AtackDistance = 0;
        private bool isSeeingPlayer = false;
        private bool isAttacking = false;
        private Animator _animator;
        private Rigidbody2D _rigidbody;
        private Vector2 _direction
        {
            get
            {
                return isRight ? Vector2.right : Vector2.left;
            }
        }

        protected void Start()
        {
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            SeePlayer();
            DefineDirection();
            Move();
        }

        void Move()
        {
            if (!isSeeingPlayer || isAttacking)
            {
                _rigidbody.StopVelocity();
                return;
            }

            if (isRight)
                _rigidbody.DefineVelocityInX(Speed);
            else
                _rigidbody.DefineVelocityInX(Speed * -1);
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
            var hit = Physics2D.Raycast(PointVision.position, _direction, DistanceDetection);

            if (hit.collider == null)
                return;

            if (hit.transform.CompareTag(ETag.PLAYER))
                OnSeeingPlayer(hit.transform.position);
            else
                isSeeingPlayer = false;
        }

        private void OnSeeingPlayer(Vector3 playerPosition)
        {
            isSeeingPlayer = true;

            var playerDistance = Vector2.Distance(transform.position, playerPosition);

            if (playerDistance <= AtackDistance)
                Attack();
            else
                isAttacking = false;
        }

        private void Attack()
        {
            isAttacking = true;
                      
        }


        //show gizmos only if object selected in scene
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawRay(PointVision.position, Vector2.right * DistanceDetection);
        }

        protected override void OnDead()
        {  
        }

        protected override void OnHitEnter(EMoveEagle direction)
        {
        }
    }
}