using Assembly_CSharp.Assets.Scripts.Enums;
using UnityEngine;

namespace Assembly_CSharp.Assets.Scripts.Components
{
    public abstract class Enemy : MonoBehaviour
    {
        protected abstract void OnHitEnter(EMoveEagle direction);
        protected abstract void OnDead();
        public float Health;

        public void OnHit(float damage, EMoveEagle direction)
        {
            OnHitEnter(direction);
            Health -= damage;

            if (Health <= 0)
                OnDead();
        }
    }
}