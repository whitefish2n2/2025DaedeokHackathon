using Unity.VisualScripting;
using UnityEngine;

namespace Core.PLayer
{
    public class Player : MonoBehaviour
    {
        public static float PlayerHeadHealth = 2f;
        public static float PlayerLeftHandHealth = 3f;
        public static float PlayerRightHandHealth = 3f;
        public static float PlayerLeftLegHealth = 3f;
        public static float PlayerRightLegHealth = 3f;
        public static float PlayerBodyHealth = 3f;

        void Update()
        {
            if (PlayerHeadHealth == 0 || PlayerBodyHealth == 0)
            {
                Debug.Log("GameOver");
                Destroy(gameObject);
            }

            if (PlayerLeftHandHealth == 0 || PlayerRightHandHealth == 0)
            {
                Debug.Log("wasd");//명중률 감소
            }
            if (PlayerLeftLegHealth == 0 || PlayerRightLegHealth == 0)
            {
                Debug.Log("wasd");//이동속도 감소
            }
        }
    }
}