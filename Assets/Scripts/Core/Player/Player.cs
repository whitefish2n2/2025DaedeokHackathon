using System;
using Codes.Util;
using Unity.VisualScripting;
using UnityEngine;

namespace Core.Player
{
    public class Player : MonoBungleton<Player>
    {
        private float playerHeadHealth = 2f;
        private float playerLeftHandHealth = 3f;
        private float playerRightHandHealth = 3f;
        private float playerLeftLegHealth = 3f;
        private float playerRightLegHealth = 3f;
        private float playerBodyHealth = 3f;

        public void ReduceDamage(string partName, float damage)
        {
            switch (partName)
            {
                case "PlayerHead":
                    playerHeadHealth -= damage;
                    break;
                case "PlayerLeftHand":
                    playerLeftHandHealth -= damage;
                    break;
                case "PlayerRightHand":
                    playerRightHandHealth -= damage;
                    break;
                case "PlayerBody":
                    playerBodyHealth -= damage;
                    break;
                case "PlayerLeftLeg":
                    playerLeftHandHealth -= damage;
                    break;
                case "PlayerRightLeg":
                    playerRightHandHealth -= damage;
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