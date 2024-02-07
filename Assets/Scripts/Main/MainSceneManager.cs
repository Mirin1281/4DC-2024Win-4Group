using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using KanKikuchi.AudioManager;

namespace Mirin
{
    public class MainSceneManager : MonoBehaviour
    {
        [SerializeField] TutorialCanvas tutorialCanvas;
        [SerializeField] Timer timer;
        [SerializeField] BallCreator ballCreator;
        [SerializeField] MouseInput mouseInput;
        [SerializeField] ScoreManager scoreManager;

        async UniTask Start()
        {
            var token = this.GetCancellationTokenOnDestroy();
            if(GamaManager.Instance.IsFirstWatchTutorial)
            {
                await tutorialCanvas.ShowTutorial();
                GamaManager.Instance.IsFirstWatchTutorial = false;
            }
            BGMManager.Instance.Play(BGMPath.FOURDC302, allowsDuplicate: true);
            ballCreator.IsLoop = true;
            timer.AddTime = true;
            mouseInput.IsLoop = true;
            await WaitTimeAsync(timer, MyHelper.GameTime, token);
            ballCreator.IsLoop = false;
            timer.AddTime = false;
            mouseInput.IsLoop = false;
            GamaManager.Instance.Score = scoreManager.Score;
            FadeLoadSceneManager.Instance.LoadScene(0.5f, "ResultScene");
        }

        async UniTask WaitTimeAsync(Timer timer, float time, CancellationToken token)
        {
            await UniTask.WaitUntil(() =>
                timer.CurrentTime > time, cancellationToken: token);
        }
    }
}