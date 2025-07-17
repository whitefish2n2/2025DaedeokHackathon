using System;
using UnityEngine;

namespace GameLogic
{
    public class TwoDSynchronization : MonoBehaviour
    {
        public GameObject target;

        private void Start()
        {
            Debug.Log(name);
        }

        private void Update()
        {
            target.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            float zRotation = transform.eulerAngles.z;
            target.transform.rotation = Quaternion.Euler(0, 0, zRotation);
        }

        private void OnDestroy()
        {
            Destroy(target);
        }
    }
}
