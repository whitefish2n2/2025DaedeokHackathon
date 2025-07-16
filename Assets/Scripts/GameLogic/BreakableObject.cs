using System;
using UnityEngine;

namespace GameLogic
{
    public class BreakableObject : Hittable
    {
        [SerializeField] private float health;
        [SerializeField] private ObjectParticle brokenObjectParticle;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private BoxCollider2D boxCollider;
        
        public override void Hit(int damage = 1)
        {
            health -= damage;
            if (health > 0) return;
            var op = Instantiate(brokenObjectParticle, transform.position, Quaternion.identity);
            op.transform.localScale = boxCollider.bounds.extents * 2;
            op.SetAverageColor(sprite.sprite, sprite.color);
            Destroy(gameObject);
        }
    }
}
