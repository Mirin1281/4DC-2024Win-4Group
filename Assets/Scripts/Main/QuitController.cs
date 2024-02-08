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
    /// �Q�[���𑦍��ɏI�����܂�
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
