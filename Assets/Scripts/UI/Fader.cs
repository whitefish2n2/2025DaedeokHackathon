using System;
using System.Collections;
using Codes.Util;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Image))]
    public class Fader : MonoBungleton<Fader>
    {
        public float fadeTime;
        public bool autoFade;
        [SerializeField] private Image fadeImage;

        private void Start()
        {
            if (autoFade) Fade(null);
        }

        public static void Fade(Action callback)
        {
            if (!IsInitialized) return;
            if (Instance.fadeImage.color.a == 0) Instance.fadeImage.enabled = true;
            var ao = Instance.fadeImage.DOFade(Instance.fadeImage.color.a == 0 ? 1 : 0, Instance.fadeTime);
            ao.OnComplete(() =>
            {
                if (Instance.fadeImage.color.a == 0) Instance.fadeImage.enabled = false;
                callback?.Invoke();
            });
        }
    }
}
