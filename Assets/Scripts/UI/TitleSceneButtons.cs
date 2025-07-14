using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TitleSceneButtons : MonoBehaviour
    {
        public Button continueButton;
        private void Start()
        {
            if (PlayerPrefs.GetInt("Progress", 0) == 0) return;
            continueButton.interactable = true;
        }

        public void GameStartButton()
        {
            Fader.Fade(() => SceneLoadingManager.Instance.LoadSceneAsync("InGame 1"));
        }

        public void ContinueButton()
        {
            Fader.Fade(() => SceneLoadingManager.Instance.LoadSceneAsync($"InGame {PlayerPrefs.GetInt("Progress", 1)}"));
        }

        public void QuitButton()
        {
            Fader.Fade(() => Application.Quit(0));
        }
    }
}
