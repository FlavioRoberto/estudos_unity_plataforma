using Assembly_CSharp.Assets.Scripts.Enums;
using UnityEngine;

namespace Assembly_CSharp.Assets.Scripts.Components
{
    public abstract class Enemy : MonoBehaviour
    {
        public float Speed = 0;
        public float Health = 0;
        public float Damage = 0;
        protected abstract void OnHitLeave(EMoveEagle direction);
        protected abstract void OnDead();

        public void OnHit(float damage, EMoveEagle direction)
        {
            Health -= damage;

            if (Health <= 0){
                OnDead();
                return;
            }

            OnHitLeave(direction);
        }

        protected void OnTriggerPlayer(Collider2D colider)
        {
            var player = colider.GetComponent<Player>();

            if (player != null)
                AttackPlayer(player);
        }

        protected void AttackPlayer(Player player)
        {
            player.OnHit(Damage);
        }

    }
}