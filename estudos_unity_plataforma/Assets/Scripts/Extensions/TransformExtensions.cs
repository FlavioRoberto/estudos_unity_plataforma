using Assembly_CSharp.Assets.Scripts.Enums;
using UnityEngine;

namespace Assembly_CSharp.Assets.Scripts.Extensions
{
    public static class TransformExtensions
    {
        public static void DefineDirectionRight(this Transform transform)
        {
            transform.eulerAngles = new Vector3(0, (float)EMoveEagle.RIGHT, 0);
        }

        public static void DefineDirectionLeft(this Transform transform)
        {
            transform.eulerAngles = new Vector3(0, (float)EMoveEagle.LEFT, 0);
        }

        public static void DefineVelocityInX(this Rigidbody2D rigidbody2D, float velocity)
        {
            rigidbody2D.velocity = new Vector2(velocity, rigidbody2D.velocity.y);
        }

        public static void StopVelocity(this Rigidbody2D rigidbody2D){
            rigidbody2D.velocity = Vector2.zero;
        }
    }
}