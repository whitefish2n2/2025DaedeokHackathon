using Unity.VisualScripting;
using UnityEngine;

namespace Core.Player
{
    public class PlayerHead : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Bullet"))
            {
                Core.PLayer.Player.PlayerHeadHealth -= 1;
                
            }
        }
    }
}