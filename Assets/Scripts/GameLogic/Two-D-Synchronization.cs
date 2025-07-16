using System;
using UnityEngine;

namespace GameLogic
{
    public class TwoDSynchronization : MonoBehaviour
    {
        public GameObject target;

        private void Update()
        {
            target.transform.localPosition = new Vector2(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y);
            float zRotation = transform.localEulerAngles.z;
            target.transform.localRotation = Quaternion.Euler(0, 0, zRotation);
        }
    }
}
