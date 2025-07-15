using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Core.Player
{
    public class Player : MonoBehaviour
    {
        private float playerHeadHealth = 2f;
        private float playerLeftHandHealth = 3f;
        private float playerRightHandHealth = 3f;
        private float playerLeftLegHealth = 3f;
        private float playerRightLegHealth = 3f;
        private float playerBodyHealth = 3f;
        public static string partName;
        

        void Update()
        {
            switch (partName)
            {
                case "PlayerHead":
                    playerHeadHealth -= 1;
                    break;
                case "PlayerLeftHand":
                    playerLeftHandHealth -= 1;
                    break;
                case "PlayerRightHand":
                    playerRightHandHealth -= 1;
                    break;
                case "PlayerBody":
                    playerBodyHealth -= 1;
                    break;
                case "PlayerLeftLeg":
                    playerLeftHandHealth -= 1;
                    break;
                case "PlayerRightLeg":
                    playerRightHandHealth -= 1;
                    break;
                
            }
            if (playerHeadHealth == 0 || playerBodyHealth == 0)
            {
                Debug.Log("GameOver");
                Destroy(gameObject);
            }

            if (playerLeftHandHealth == 0 || playerRightHandHealth == 0)
            {
                Debug.Log("wasd");//명중률 감소
            }
            if (playerLeftLegHealth == 0 || playerRightLegHealth == 0)
            {
                Debug.Log("wasd");//이동속도 감소
            }
        }
        
    }
}