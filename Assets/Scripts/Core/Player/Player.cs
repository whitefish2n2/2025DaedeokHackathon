using System;
using Codes.Util;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

namespace Core.Player
{
    public class Player : MonoBungleton<Player>
    {
        private int playerHeadHealth = 2;
        private int playerLegHealth = 3;
        private int playerBodyHealth = 5;
        private PlayerMove playerMove;
        

        protected override void Initialize()
        {
            playerMove = GetComponent<PlayerMove>();
            if (playerMove == null) Debug.LogError("playerMove is null");
        }

        public void ReduceDamage(string partName, int damage)
        {
            switch (partName)
            {
                case "Head2DSync":
                    Debug.Log("Hit Head");
                    playerHeadHealth -= damage;
                    break;
                case "Body2DSync":
                    Debug.Log("Hit Body");
                    playerBodyHealth -= damage;
                    break;
                case "Lag2DSync":
                    Debug.Log("Hit Lag");
                    playerLegHealth -= damage;
                    break;
            }
            
            if (playerHeadHealth == 0 || playerBodyHealth == 0)
            {
                Debug.Log("GameOver");
                Destroy(gameObject);
            }

            if (playerBodyHealth == 3)
            {
                Debug.Log("wasd");//명중률 감소
            }
            if (playerLegHealth ==0)
            {
                Debug.Log("wasd");//이동속도 감소
            }
        }
    }
}