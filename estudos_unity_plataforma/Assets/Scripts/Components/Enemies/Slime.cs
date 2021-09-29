using Assembly_CSharp.Assets.Scripts.Enums;
using Assembly_CSharp.Assets.Scripts.Extensions;
using UnityEngine;

namespace Assembly_CSharp.Assets.Scripts.Components
{
    public class Slime : MonoBehaviour
    {
        public float Speed;
        private Rigidbody2D _rigidbody;

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            _rigidbody.velocity = new Vector2(Speed, _rigidbody.velocity.y);
        }

        void OnTriggerEnter2D(Collider2D colider)
        {
            if (colider.gameObject.layer == (int)ELayer.WALL)
            {
                Speed = Speed * -1;
            }

            DefineDeirection();
        }

        private void DefineDeirection()
        {
            if (Speed < 0)
                transform.DefineDirectionRight();
            else
                transform.DefineDirectionLeft();
        }

    }
}