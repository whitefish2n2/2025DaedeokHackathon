using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActiveItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemAmountText;
    [SerializeField] private Image itemCoolDownMaskImage;
    [SerializeField] private Button itemButton;

    public void SetItemAmount(int amount)
    {
        itemAmountText.text = amount.ToString();
        if (amount == 0)
        {
            itemButton.interactable = false;
        }
        else
            itemButton.interactable = true;
    }

    public void SetItemCoolDown(float coolDown)
    {
        itemCoolDownMaskImage.fillAmount = 1-coolDown;
    }
}
