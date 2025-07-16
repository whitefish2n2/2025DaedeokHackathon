using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Util;

namespace UI
{
    public class TitleSceneButtons : MonoBehaviour
    {
        public Button continueButton;
        public EventTrigger continueTrigger;
        private void Start()
        {
            if (PlayerPrefs.GetInt("Progress", 0) == 0) return;
            continueButton.interactable = true;
            continueTrigger.triggers[0].callback.AddListener(_ => OnMouseEnter());
        }

        public void OnMouseEnter()
        {
            AudioManager.Instance.PlaySound(SoundType.ButtonHover);
        }

        public void GameStartButton()
        {
            Fader.Fade(() => SceneLoadingManager.Instance.LoadSceneWithLoadingScene("InGame 1", "LoadingScene"));
        }

        public void ContinueButton()
        {
            Fader.Fade(() => SceneLoadingManager.Instance.LoadSceneWithLoadingScene($"InGame {PlayerPrefs.GetInt("Progress", 1)}", "LoadingScene"));
        }

        public void QuitButton()
        {
            Fader.Fade(() => Application.Quit(0));
        }
    }
}
