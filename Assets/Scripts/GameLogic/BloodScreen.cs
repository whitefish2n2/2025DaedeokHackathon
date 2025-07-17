using UnityEngine;
using UnityEngine.UI;
public class BloodScreen : MonoBehaviour
{
    private Image image;
    public static bool hit; //플레이어 피격시
    private float hitTime;
    void Start()
    {
        image = GetComponent<Image>();
        hitTime = 0;
    }
    void Update()
    {
        Color color = image.color;
        if (hit)
        {
            color.a = 255;
            hitTime += 1;
        }

        if (hitTime > 100)
        {
            hit = false;
        }
        if (!hit && hitTime > 100)
        {
            color.a = 0;
            hitTime = 0;
        }
        image.color = color;
    }
}