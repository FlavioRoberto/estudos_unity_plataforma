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

    }
}