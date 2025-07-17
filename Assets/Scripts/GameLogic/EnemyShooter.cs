using System;
using System.Collections;
using GameLogic;
using UnityEngine;
using Util;

public class EnemyShooter : MonoBehaviour
{
    [Range(0.2f,100f)] public float termToShot;
    public bool alive;

    public bool shotToRight;

    public GameObject bullet;

    [SerializeField] private bool testMode;//start에서 발사

    private void Start()
    {
        if(testMode)
            StartShot();
    }

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
        AudioManager.Instance.PlaySound(SoundType.BulletShot);
        var b = Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation);
        b.GetComponent<Bullet>().Shot(shotToRight);
        Destroy(b,3f);
        var o =Physics2D.Raycast(transform.position, Vector2.right * (shotToRight?1:-1), 100f,LayerMask.GetMask("Hitable"));
        if(o.transform&& o.transform.gameObject.TryGetComponent<Hittable>(out var hittable))
        {
            hittable.Hit(1);
        }
    }
}
