using System;
using System.Collections;
using System.Collections.Generic;
using Core.Player;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using Util;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private float shotCoolDown;
    private float currentShotCoolDown;
    private float cooldownProgress = 1;
    public int leftBullet;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject gunShotOrigin;
    public bool canShot = true;
    [Header("Heal")]
    [SerializeField] private float healCoolDown;
    [SerializeField]public int leftHeal;
    [SerializeField]public float healTime;
    [SerializeField]private float currentHealCoolDown;
    [SerializeField]private float healCooldownProgress = 1;
    [SerializeField]public bool canHeal = true;
    

    [SerializeField]private bool zoomed;

    [SerializeField]private bool healing;
    
    private PlayerMove playerMove;
    
    [SerializeField]private Vector3 shotPosition;//플레이어 기준

    private void Start()
    {
        ActiveItemUIUpdater.Instance.SetBullet(leftBullet);
        ActiveItemUIUpdater.Instance.SetBulletProgress(1);
        ActiveItemUIUpdater.Instance.SetHeal(leftHeal);
        ActiveItemUIUpdater.Instance.SetHealProgress(1);
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
                Debug.Log("Healing Cancel");
                CancelHeal();
                healing = false;
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
                AudioManager.Instance.PlaySound(SoundType.BulletShot);
                
                var b = Instantiate(bullet, gunShotOrigin.transform.position,
                Quaternion.identity);
//                Debug.Log(gunShotOrigin.transform.position);
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
        if (leftHeal == 0) return;
        if (canHeal)
        {
            canceled = false;
            Debug.Log("Healing");
            healing = true;
            playerMove?.Heal();
            StartCoroutine(HealingFlow());
            AudioManager.Instance.PlaySound(SoundType.Bandages);
        }
    }

    [SerializeField]private bool canceled = false;
    public void CancelHeal()
    {
        if (healing)
        {
            ActionBar.Instance.DoneProgress();
            playerMove?.CancelHeal();
            canceled = true;
            healing = false;
        }
        
    }

    IEnumerator HealingFlow()
    {
        float elapsedTime = 0;
        while (canceled == false && elapsedTime < shotCoolDown)
        {
            elapsedTime += Time.deltaTime;
            ActionBar.Instance?.SetProgress(elapsedTime / healTime);
            yield return null;
        }
        if (canceled == true)
        {
            ActiveItemUIUpdater.Instance.SetHeal(leftHeal);
            ActiveItemUIUpdater.Instance.SetHealProgress(1);
        }
        else
        {
            Player.Instance?.Heal(1);
            Debug.Log("Healing Success");
            leftHeal--;
            ActiveItemUIUpdater.Instance.SetHeal(leftHeal);
            ActiveItemUIUpdater.Instance.SetHealProgress(1);
            playerMove.CancelHeal();
            StartCoroutine(HealCoolDownFlow());
            ActionBar.Instance?.DoneProgress();
            healing = false;
        }
    }
    IEnumerator ShotCoolDownFlow()
    {
        canShot = false;
        float elapsedTime = 0;
        cooldownProgress = 0;
        while (elapsedTime < shotCoolDown)
        {
            elapsedTime += Time.deltaTime;
            cooldownProgress = Mathf.Clamp(elapsedTime / shotCoolDown, 0, 1);
            ActiveItemUIUpdater.Instance.SetBulletProgress(cooldownProgress);
            yield return null;
        }
        cooldownProgress = 1;
        yield return null;
        canShot = true;
    }
    
    IEnumerator HealCoolDownFlow()
    {
        canHeal = false;
        float elapsedTime = 0;
        healCooldownProgress = 0;
        while (elapsedTime < healCoolDown)
        {
            elapsedTime += Time.deltaTime;
            healCooldownProgress = Mathf.Clamp(elapsedTime / healCoolDown, 0, 1);
            ActiveItemUIUpdater.Instance.SetHealProgress(healCooldownProgress);
            yield return null;
        }
        healCooldownProgress = 1;
        yield return null;
        canHeal = true;
    }
}
