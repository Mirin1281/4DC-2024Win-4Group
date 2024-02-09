using UnityEngine;
using Cysharp.Threading.Tasks;
using KanKikuchi.AudioManager;

namespace Mirin
{
    public class MainSceneManager : MonoBehaviour
    {
        [SerializeField] Texture2D cursorTexture;
        [SerializeField] TutorialCanvas tutorialCanvas;
        [SerializeField] ReadyCanvas readyCanvas;
        [SerializeField] Canvas finishCanvas;
        [SerializeField] Timer timer;
        [SerializeField] BallCreator ballCreator;
        [SerializeField] MouseInput mouseInput;
        [SerializeField] ScoreManager scoreManager;

        async UniTask Start()
        {
#if UNITY_EDITOR
            Application.targetFrameRate = 60;
#endif
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
            if (GameManager.Instance.IsFirstWatchTutorial)
            {
                await tutorialCanvas.ShowTutorial();
                GameManager.Instance.IsFirstWatchTutorial = false;
            }
            await readyCanvas.ShowReady();

            BGMManager.Instance.Play(BGMPath.AWARENESS_F, volumeRate: 0.5f, allowsDuplicate: true);
            ballCreator.IsLoop = true;
            timer.AddTime = true;
            mouseInput.IsLoop = true;
            await UniTask.WaitUntil(() => timer.CurrentTime > MyHelper.GameTime);

            finishCanvas.gameObject.SetActive(true);
            SEManager.Instance.PlaySE(SEType.Finish);
            ballCreator.IsLoop = false;
            timer.AddTime = false;
            mouseInput.IsLoop = false;
            GameManager.Instance.Score = scoreManager.Score;
            await MyHelper.WaitSeconds(2f, default);
            await FadeLoadSceneManager.Instance.LoadSceneAsync(0.5f, "Result");
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            BGMManager.Instance.Stop();
        }
    }
}