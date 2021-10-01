using Assembly_CSharp.Assets.Scripts.Enums;
using Assembly_CSharp.Assets.Scripts.Extensions;
using UnityEngine;

namespace Assembly_CSharp.Assets.Scripts.Components
{
    public class Slime : Enemy
    {
        private Animator _animator;
        private Rigidbody2D _rigidbody;

        protected void Start()
        {
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            _rigidbody.velocity = new Vector2(Speed, _rigidbody.velocity.y);
        }

        void OnTriggerEnter2D(Collider2D colider)
        {
            OnTriggerPlayer(colider);

            if (colider.gameObject.layer == (int)ELayer.WALL)
                Speed = Speed * -1;

            DefineDirection();
        }

        private void DefineDirection()
        {
            if (Speed < 0)
                transform.DefineDirectionRight();
            else
                transform.DefineDirectionLeft();
        }

        protected override void OnHitEnter(EMoveEagle direction)
        {
            if (direction == EMoveEagle.RIGHT)
                _rigidbody.AddForce(Vector2.right * 1300, ForceMode2D.Force);
            else
                _rigidbody.AddForce(Vector2.left * 1300, ForceMode2D.Force);

            _animator.SetTrigger(ETrigger.HIT);
        }

        protected override void OnDead()
        {
            _animator.SetTrigger(ETrigger.DEAD);
            Speed = 0;
            Destroy(gameObject, 0.5f);
        }
    }
}