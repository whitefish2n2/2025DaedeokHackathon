using UnityEngine;
using UnityEngine.UI;

public class HealProgressBarUI : MonoBehaviour
{
    public GameObject barOrigin;
    public Image percentImage;

    public void SetPercent(float percent)
    {
        percentImage.fillAmount = percent;
    }

    public void Active(bool active)
    {
        barOrigin.SetActive(active);
    }
}
