using System;
using Codes.Util;
using Core.Player;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionAnnotationPosManager : MonoBungleton<InteractionAnnotationPosManager>
{
    public Vector3 offsetByPlayer;
    public TextMeshProUGUI annotationText;
    public Image annotationImage;
    public Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
        DisappearAnnotation();
    }

    public void SetText(string text)
    {
        annotationText.text = text;
    }

    public void AppearAnnotation()
    {
        annotationImage.DOFade(1, 0.3f);
        annotationText.DOFade(1, 0.3f);
    }

    public void DisappearAnnotation()
    {
        annotationImage.DOFade(0, 0.3f);
        annotationText.DOFade(0, 0.3f);
    }

    private void Update()
    {
        transform.position = mainCam.WorldToScreenPoint(Player.Instance.transform.position + offsetByPlayer);
    }
}
