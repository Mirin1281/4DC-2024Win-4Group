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
        [SerializeField] MouseInput mouseInput;

        async UniTask Start()
        {
            var token = this.GetCancellationTokenOnDestroy();
            await tutorialCanvas.ShowTutorial();
            ballCreator.IsLoop = true;
            timer.AddTime = true;
            mouseInput.IsLoop = true;
            await WaitTimeAsync(timer, MyHelper.GameTime, token);
            ballCreator.IsLoop = false;
            timer.AddTime = false;
            mouseInput.IsLoop = false;
        }

        async UniTask WaitTimeAsync(Timer timer, float time, CancellationToken token)
        {
            await UniTask.WaitUntil(() =>
                timer.CurrentTime > time, cancellationToken: token);
        }
    }
}