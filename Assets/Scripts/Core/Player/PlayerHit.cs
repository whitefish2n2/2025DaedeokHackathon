using UnityEngine;

namespace Core.Player
{
    public class PlayerHit : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Bullet"))
            {
                Player.partName = gameObject.name;
            }
        }
    }   
}
