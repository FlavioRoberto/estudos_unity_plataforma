using Assembly_CSharp.Assets.Scripts.Enums;
using UnityEngine;

namespace Assembly_CSharp.Assets.Scripts.Components
{
    public abstract class Enemy : MonoBehaviour
    {
        public float Speed;
        public float Health;
        public float Damage;
        protected abstract void OnHitEnter(EMoveEagle direction);
        protected abstract void OnDead();
          
        public void OnHit(float damage, EMoveEagle direction)
        {
            OnHitEnter(direction);
            Health -= damage;

            if (Health <= 0)
                OnDead();
        }


        protected void OnTriggerPlayer(Collider2D colider)
        {
            var player = colider.GetComponent<Player>();

            if (player != null)
                player.OnHit(Damage);

        }

    }
}