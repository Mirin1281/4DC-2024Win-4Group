using UnityEngine;

public class QuitController : SingletonMonoBehaviour<QuitController>
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    /// <summary>
    /// ゲームを即座に終了します
    /// </summary>
    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
