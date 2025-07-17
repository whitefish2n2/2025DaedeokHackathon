using System;
using UnityEngine;

public class InGame1Manager : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.SetInt("Progress",1);
    }

    public void NextStage()
    {
        SceneLoadingManager.Instance.LoadSceneWithLoadingScene("InGame 2");
    }
}
