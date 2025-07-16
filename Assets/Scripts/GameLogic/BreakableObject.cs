using System;
using UnityEngine;

namespace GameLogic
{
    public class BreakableObject : MonoBehaviour
    {
        [SerializeField] private float health;
        [SerializeField] private ObjectParticle brokenObjectParticle;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private BoxCollider2D boxCollider;
        [Obsolete("Obsolete")]
        private void OnCollisionEnter2D(Collision2D other)
        {
            // todo: 추후 총알 태그로 변경
            if (!other.collider.CompareTag("Bullet")) return;
            health -= 1;
            if (health > 0) return;
            var op = Instantiate(brokenObjectParticle, transform.position, Quaternion.identity);
            op.transform.localScale = boxCollider.bounds.extents * 2;
            op.SetAverageColor(sprite.sprite, sprite.color);
            Destroy(gameObject);
        }
    }
}
