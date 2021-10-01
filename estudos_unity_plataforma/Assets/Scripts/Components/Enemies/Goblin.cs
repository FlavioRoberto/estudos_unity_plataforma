using Assembly_CSharp.Assets.Scripts.Enums;
using UnityEngine;

namespace Assembly_CSharp.Assets.Scripts.Components
{
    public class Goblin : Enemy
    {
        public float DistanceDetection = 1;
        public Transform PointVision;
        private bool isSeeingPlayer;
        private Animator _animator;
        private Rigidbody2D _rigidbody;

        protected void Start()
        {
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            SeePlayer();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(PointVision.position, Vector2.right * DistanceDetection);
        }

        void SeePlayer()
        {
            var hit = Physics2D.Raycast(PointVision.position, Vector2.right, DistanceDetection);

            if (hit.collider == null)
                return;

            if(hit.transform.CompareTag(ETag.PLAYER))
                OnSeeingPlayer();
        }
        
        private void OnSeeingPlayer(){
            
        }

        protected override void OnDead()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnHitEnter(EMoveEagle direction)
        {
            throw new System.NotImplementedException();
        }


    }
}