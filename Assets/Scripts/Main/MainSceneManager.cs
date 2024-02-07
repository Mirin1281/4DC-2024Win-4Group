using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Mirin
{
    public class MainSceneManager : MonoBehaviour
    {
        [SerializeField] TutorialCanvas tutorialCanvas;
        [SerializeField] Timer timer;
        [SerializeField] BallCreator ballCreator;

        async UniTask Start()
        {
            var token = this.GetCancellationTokenOnDestroy();
            await tutorialCanvas.ShowTutorial();
            ballCreator.LoopCreateAsync().Forget();
            timer.AddTime = true;
            await WaitTimeAsync(timer, MyHelper.GameTime, token);
        }

        async UniTask WaitTimeAsync(Timer timer, float time, CancellationToken token)
        {
            await UniTask.WaitUntil(() =>
                timer.CurrentTime > time, cancellationToken: token);
        }
    }
}