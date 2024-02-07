using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Mirin
{
    public class BallCreator : MonoBehaviour
    {
        [SerializeField] BallPool ballPool;
        [SerializeField] float speed = 1f;

        public bool IsFever { get; set; }

        bool isLoop;
        public bool IsLoop
        {
            get => isLoop;
            set
            {
                isLoop = value;
                if (value == true)
                {
                    LoopCreateAsync().Forget();
                }
            }
        }

        async UniTask LoopCreateAsync()
        {
            var token = this.GetCancellationTokenOnDestroy();
            while (IsLoop)
            {
                if(IsFever)
                {
                    var ball = ballPool.GetBall(BallSpriteType.Red1);
                    BallMoveInFever(ball, token).Forget();
                    await MyHelper.WaitSeconds(0.05f, token);
                }
                else
                {
                    var ball = ballPool.GetBall(BallSpriteType.Blue1);
                    BallMove(ball, token).Forget();
                    await MyHelper.WaitSeconds(0.1f, token);
                }
            }
        }

        async UniTask BallMove(Ball ball, CancellationToken token)
        {
            var randX = Random.Range(-7.5f, 7.5f);
            float t = 0f;
            while(t < 5f && ball.gameObject.activeInHierarchy)
            {
                ball.transform.localPosition = new Vector3(randX, 7 - t * t * speed);
                t += Time.deltaTime;
                await UniTask.Yield(token);
            }
            ball.gameObject.SetActive(false);
        }

        async UniTask BallMoveInFever(Ball ball, CancellationToken token)
        {
            var randX = Random.Range(-7.5f, 7.5f);
            float t = 0f;
            while (t < 5f && ball.gameObject.activeInHierarchy)
            {
                ball.transform.localPosition = new Vector3(randX, 7 - t * t * speed * 2f);
                t += Time.deltaTime;
                await UniTask.Yield(token);
            }
            ball.gameObject.SetActive(false);
        }
    }
}