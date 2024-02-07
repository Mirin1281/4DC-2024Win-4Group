using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using KanKikuchi.AudioManager;

namespace Mirin
{
    public class MainSceneManager : MonoBehaviour
    {
        [SerializeField] TutorialCanvas tutorialCanvas;
        [SerializeField] ReadyCanvas readyCanvas;
        [SerializeField] Canvas finishCanvas;
        [SerializeField] Timer timer;
        [SerializeField] BallCreator ballCreator;
        [SerializeField] MouseInput mouseInput;
        [SerializeField] ScoreManager scoreManager;

        async UniTask Start()
        {
            if(GamaManager.Instance.IsFirstWatchTutorial)
            {
                await tutorialCanvas.ShowTutorial();
                GamaManager.Instance.IsFirstWatchTutorial = false;
            }
            await readyCanvas.ShowReady();

            BGMManager.Instance.Play(BGMPath.FOURDC302, allowsDuplicate: true);
            ballCreator.IsLoop = true;
            timer.AddTime = true;
            mouseInput.IsLoop = true;
            var token = this.GetCancellationTokenOnDestroy();
            await WaitTimeAsync(timer, MyHelper.GameTime, token);

            finishCanvas.gameObject.SetActive(true);
            SEManager.Instance.PlaySE(SEType.Finish);
            ballCreator.IsLoop = false;
            timer.AddTime = false;
            mouseInput.IsLoop = false;
            GamaManager.Instance.Score = scoreManager.Score;
            await MyHelper.WaitSeconds(2f, default);
            FadeLoadSceneManager.Instance.LoadScene(0.5f, "Result");
        }

        async UniTask WaitTimeAsync(Timer timer, float time, CancellationToken token)
        {
            await UniTask.WaitUntil(() =>
                timer.CurrentTime > time, cancellationToken: token);
        }
    }
}