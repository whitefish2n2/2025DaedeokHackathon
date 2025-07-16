using UnityEngine;

namespace GameLogic
{
    public abstract class Hittable : MonoBehaviour
    {
        public abstract void Hit(float damage = 1f);
    }
}
