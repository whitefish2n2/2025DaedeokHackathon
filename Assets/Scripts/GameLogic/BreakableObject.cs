using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic
{
    public class BreakableObject : Hittable
    {
        private float _maxHealth;
        private Vector3[] _positions;
        [SerializeField] private float health;
        [SerializeField] private ObjectParticle brokenObjectParticle;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private BoxCollider2D boxCollider;
        [SerializeField] private LineRenderer healthBar;
        [SerializeField] private LineRenderer healthBarBackground;

        private void Start()
        {
            healthBar.transform.localPosition = new Vector3(0, boxCollider.bounds.extents.y + 0.3f, 0);
            healthBarBackground.transform.localPosition = new Vector3(0, boxCollider.bounds.extents.y + 0.3f, 0);
            _positions = new[] {
                new Vector3(-boxCollider.bounds.extents.x - 0.1f, 0, 0),
                new Vector3(boxCollider.bounds.extents.x + 0.2f, 0, 0),
            };
            healthBar.SetPositions(_positions);
            healthBarBackground.SetPositions(_positions);
            _maxHealth = health;
        }

        public override void Hit(int damage = 1)
        {
            health -= damage;
            _positions[1].x = Mathf.Lerp(-boxCollider.bounds.extents.x - 0.1f, boxCollider.bounds.extents.x + 0.2f, health / _maxHealth);
            healthBar.SetPositions(_positions);
            if (health > 0) return;
            var op = Instantiate(brokenObjectParticle, transform.position, Quaternion.identity);
            op.transform.localScale = boxCollider.bounds.extents * 2;
            op.SetAverageColor(sprite.sprite, sprite.color);
            Destroy(gameObject);
        }
        
        public void InstantBreak()
        {
            health = 0;
            _positions[1].x = Mathf.Lerp(-boxCollider.bounds.extents.x - 0.1f, boxCollider.bounds.extents.x + 0.2f, health / _maxHealth);
            healthBar.SetPositions(_positions);
            if (health > 0) return;
            var op = Instantiate(brokenObjectParticle, transform.position, Quaternion.identity);
            op.transform.localScale = boxCollider.bounds.extents * 2;
            op.SetAverageColor(sprite.sprite, sprite.color);
            Destroy(gameObject);
        }
    }
}
