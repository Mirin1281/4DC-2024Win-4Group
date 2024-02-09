using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// LoadTitleScene�Ƃ�������MainScene�ւ��ړ�����X�N���v�g
/// </summary>

public class LoadTitleScene : MonoBehaviour
{
    public string TitleSceneName = "Title";
    public string MainSceneName = "MainScene";

    public void LoadTitle()
    {
        SceneManager.LoadScene(TitleSceneName);
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(MainSceneName);
    }
}
