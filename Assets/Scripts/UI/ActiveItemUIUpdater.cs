using Plugins;
using UnityEngine;

public class ActiveItemUIUpdater : MonoSingleton<ActiveItemUIUpdater>
{
    [SerializeField] private ActiveItemUI bulletUI;
    [SerializeField] private ActiveItemUI healUI;
    [SerializeField] private ActiveItemUI nadeUI;
    public void SetBullet(int amount)
    {
        bulletUI?.SetItemAmount(amount);
    }

    public void SetBulletProgress(float value)
    {
        bulletUI?.SetItemCoolDown(value);
    }

    public void SetHeal(int amount)
    {
        healUI?.SetItemAmount(amount);
    }
    
    public void SetHealProgress(float value)
    {
        healUI?.SetItemCoolDown(value);
    }

    public void SetNade(int amount)
    {
        nadeUI?.SetItemAmount(amount);
    }

    public void SetNadeProgress(float value)
    {
        nadeUI?.SetItemCoolDown(value);
    }
}
