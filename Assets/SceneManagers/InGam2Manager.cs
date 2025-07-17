using System;
using UnityEngine;

public class InGame2Manager : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.SetInt("Progress",2);
    }

    public void NextStage()
    {
        SceneLoadingManager.Instance.LoadSceneWithLoadingScene("TitleScene");
    }
}
