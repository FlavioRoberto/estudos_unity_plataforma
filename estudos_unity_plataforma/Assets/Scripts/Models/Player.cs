using System;
using UnityEngine;

namespace Assembly_CSharp.Assets.Scripts.Models
{
    public class Player : MonoBehaviour
    {
        public float Health { get; private set; }
        public float Speed { get; private set; }
        public float JumpForce { get; private set; }
        public float Damage { get; private set; }
        public float RecoverTime { get; private set; }
        public int CountJump { get; private set; }
        public bool isAttacking { get; private set; }
        public bool isDead { get; private set; }
        public bool isMoving { get; private set; }
        public bool CanJump { get { return CountJump < 2; } }
        public bool InGround { get { return CountJump == 0; } }

        public Player()
        {
            Health = 3;
            Speed = 3f;
            JumpForce = 10;
            Damage = 1;
            RecoverTime = 0;
            CountJump = 0;
            isAttacking = false;
            isDead = false;
            isMoving = false;
        }

        public void SetHealth(int health) {
            Health = health;
        }

        public void DecreaseRecoverTime()
        {
            RecoverTime -= Time.deltaTime;
        }

        public void Move()
        {
            isMoving = true;
        }

        public void StopMove()
        {
            isMoving = false;
        }

        public void Attack()
        {
            isAttacking = true;
        }

        public void Jump()
        {
            CountJump += 1;
        }

        public void StopAttack()
        {
            isAttacking = false;
        }

        public void SetInGround()
        {
            CountJump = 0;
        }

        public void OnHit(float damage, Action hit, Action dead)
        {
            if (isDead)
                return;

            if (RecoverTime <= 0)
            {
                Health -= damage;
                RecoverTime = 1;
                hit();
            }

            if (Health <= 0)
                OnDead(dead);
        }

        private void OnDead(Action dead)
        {
            dead();
            isDead = true;
        }
    }
}

