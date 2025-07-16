using GameLogic;
using UnityEngine;

namespace Core.Player
{
    public class PlayerHit : Hittable
    {
        public override void Hit(float damage = 1f)
        {
            Player.Instance.ReduceDamage(gameObject.name, damage);
        }
    }   
}
