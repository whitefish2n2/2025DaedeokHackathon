using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3f;
    public void Shot(bool isRight)
    {
        transform.DOMove(transform.position + 100*(isRight?1:-1)*Vector3.right, lifeTime);
    }

}
