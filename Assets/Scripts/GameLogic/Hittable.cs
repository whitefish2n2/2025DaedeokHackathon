using UnityEngine;

namespace GameLogic
{
    public abstract class Hittable : MonoBehaviour
    {
        public abstract void Hit(int damage = 1);
    }
}
