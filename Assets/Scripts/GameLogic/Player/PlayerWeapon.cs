using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private float shotCoolDown;
    private float currentShotCoolDown;
    private float cooldownPrograss = 1;
    public int leftBullet;
    public Image coolDownImage;
    public Image shotImage;
    public bool canShot = true;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject gunShotOrigin;

    private bool zoomed;
    
    private PlayerMove playerMove;
    
    [SerializeField]private Vector3 shotPosition;//플레이어 기준

    private void Start()
    {
        playerMove = GetComponent<PlayerMove>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            zoomed = true;
            playerMove?.Zoom(true);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            zoomed = false;
            playerMove?.Zoom(false);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (zoomed)
            {
                if (leftBullet == 0||!canShot) return;
                playerMove?.Zoom(false);
                
                var b = Instantiate(bullet, gunShotOrigin.transform.position,
                Quaternion.identity);
                Debug.Log(gunShotOrigin.transform.position);
                b.GetComponent<Bullet>().Shot(playerMove?.watchingRight??true);
                Destroy(b,3f);
                leftBullet--;
                StartCoroutine(CoolDownFlow());
            }
                
        }
    }

    IEnumerator CoolDownFlow()
    {
        canShot = false;
        float elapsedTime = 0;
        cooldownPrograss = 0;
        while (elapsedTime < shotCoolDown)
        {
            elapsedTime += Time.deltaTime;
            cooldownPrograss = Mathf.Clamp(elapsedTime / shotCoolDown, 0, 1);
            yield return null;
        }
        cooldownPrograss = 1;
        yield return null;
        canShot = true;
    }
}
