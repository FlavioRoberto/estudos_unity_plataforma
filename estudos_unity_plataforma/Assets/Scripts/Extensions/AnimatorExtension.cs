using Assembly_CSharp.Assets.Scripts.Enums;
using UnityEngine;

namespace Assembly_CSharp.Assets.Scripts.Extensions
{
    public static class AnimatorExtension
    {
        public static void SetTransition(this Animator animator, EPlayerTransition transition)
        {
            animator.SetInteger("PlayerTransition", (int)transition);
        }

        public static void SetMovePosition(this Animator animator, EMoveEagle move)
        {
            animator.SetTransition(EPlayerTransition.RUN);
            animator.transform.eulerAngles = new Vector3(0, (int)move, 0);
        }
    }
}
