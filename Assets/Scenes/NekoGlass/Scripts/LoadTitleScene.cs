using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTitleScene : MonoBehaviour
{
    public string TitleSceneName = "Title";

    public void LoadTitle()
    {
        SceneManager.LoadScene(TitleSceneName);
    }
}
