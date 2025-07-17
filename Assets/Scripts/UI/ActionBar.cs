using System;
using Codes.Util;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ActionBar : MonoBungleton<ActionBar>
{
    [SerializeField] private Image motherBar;
    [SerializeField] private Image bar;
    private bool isProgressing;

    private void Start()
    {
        motherBar.DOFade(0, 0f);
        bar.DOFade(0, 0f);
        bar.fillAmount = 0f;
    }

    public void SetProgress(float progress)
    {
        bar.fillAmount = progress;
        if (!isProgressing)
        {
            isProgressing = true;
            motherBar.DOFade(1, 0.5f);
            bar.DOFade(1, 0.5f);
        }
    }

    public void DoneProgress()
    {
        motherBar.DOFade(0, 0.5f);
        bar.DOFade(0, 0.5f);
        isProgressing = false;
    }
}
