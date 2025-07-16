using System;
using System.Collections;
using GameLogic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [Range(0.2f,100f)] public float termToShot;
    public bool alive;

    public bool shotToRight;

    public GameObject bullet;

    public void StartShot()
    {
        StartCoroutine(ShotFlow());
    }

    IEnumerator ShotFlow()
    {
        while (alive)
        {
            Shot();
            yield return new WaitForSeconds(termToShot);
        }
        yield return null;
    }

    public void Shot()
    {
        var b = Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation);
        b.GetComponent<Bullet>().Shot(shotToRight);
        var o =Physics2D.Raycast(transform.position, Vector2.right * (shotToRight?1:-1), 100f,LayerMask.GetMask("Hitable"));
        if(o.transform&& o.transform.gameObject.TryGetComponent<Hittable>(out var hittable))
        {
            hittable.Hit(1);
        }
    }
}
