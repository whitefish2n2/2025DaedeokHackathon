using GameLogic;
using UnityEngine;

namespace Core.Player
{
    public class PlayerHit : Hittable
    {
        public override void Hit(int damage = 1)
        {
            Player.Instance.ReduceDamage(gameObject.name, damage);
        }
    }   
}
