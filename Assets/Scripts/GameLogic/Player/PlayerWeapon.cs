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
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject gunShotOrigin;
    public bool canShot = true;
    [Header("Heal")]
    [SerializeField] private float healCoolDown;
    public int leftHeal;
    private float currentHealCoolDown;
    private float healCooldownPrograss = 1;
    public bool canHeal = true;
    

    private bool zoomed;

    private bool healing;
    
    private PlayerMove playerMove;
    
    [SerializeField]private Vector3 shotPosition;//플레이어 기준

    private void Start()
    {
        ActiveItemUIUpdater.Instance.SetBullet(leftBullet);
        ActiveItemUIUpdater.Instance.SetBulletProgress(1);
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
        else if (Input.GetKeyDown(KeyCode.V))
        {
            if(healing)
            {
                CancelHeal();
                return;
            }
            healing = true;
            
            Heal();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (zoomed)
            {
                if (leftBullet == 0||!canShot) return;
                playerMove?.Shot();
                
                var b = Instantiate(bullet, gunShotOrigin.transform.position,
                Quaternion.identity);
                Debug.Log(gunShotOrigin.transform.position);
                b.GetComponent<Bullet>().Shot(playerMove?.watchingRight??true);
                Destroy(b,3f);
                leftBullet--;
                ActiveItemUIUpdater.Instance.SetBullet(leftBullet);
                StartCoroutine(ShotCoolDownFlow());
            }
                
        }
    }

    public void Heal()
    {
        if (canHeal)
        {
            playerMove?.Heal();
        }
    }
    public void CancelHeal()
    {
        playerMove?.CancelHeal();
    }

    IEnumerator ShotCoolDownFlow()
    {
        canShot = false;
        float elapsedTime = 0;
        cooldownPrograss = 0;
        while (elapsedTime < shotCoolDown)
        {
            elapsedTime += Time.deltaTime;
            cooldownPrograss = Mathf.Clamp(elapsedTime / shotCoolDown, 0, 1);
            ActiveItemUIUpdater.Instance.SetBulletProgress(cooldownPrograss);
            yield return null;
        }
        cooldownPrograss = 1;
        yield return null;
        canShot = true;
    }
    
    IEnumerator HealCoolDownFlow()
    {
        canHeal = false;
        float elapsedTime = 0;
        healCooldownPrograss = 0;
        while (elapsedTime < healCoolDown)
        {
            elapsedTime += Time.deltaTime;
            healCooldownPrograss = Mathf.Clamp(elapsedTime / healCoolDown, 0, 1);
            ActiveItemUIUpdater.Instance.SetBulletProgress(healCooldownPrograss);
            yield return null;
        }
        healCooldownPrograss = 1;
        yield return null;
        canHeal = true;
    }
}
