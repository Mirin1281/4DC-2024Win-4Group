using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Mirin
{
    public class BallCreator : MonoBehaviour
    {
        [SerializeField] BallPool ballPool;
        [SerializeField] BallSpriteTypeDat typeData;

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
                var randSize = Random.Range(0.1f, 0.5f);
                var randTypeNum = Random.Range(1, 101);
                var randRotate = Random.Range(0f, 360f);
                if (IsFever)
                {
                    var type = randTypeNum switch
                    {
                        <= 10 => BallSpriteType.Out1,
                        <= 30 => BallSpriteType.Out2,
                        _ => BallSpriteType.Out3,
                    };
                    var ball = ballPool.GetBall(type);
                    ball.SetSize(randSize);
                    ball.SetRotate(randRotate);
                    var randSpeed = Random.Range(4f, 10f);
                    BallMoveInFever(ball, randSpeed, token).Forget();
                    await MyHelper.WaitSeconds(0.03f, token);
                }
                else
                {
                    var type = randTypeNum switch
                    {
                        <= 3 => BallSpriteType.Yellow1,
                        <= 10 => BallSpriteType.Red1,
                        <= 30 => BallSpriteType.Purple1,
                        _ => BallSpriteType.Blue1,
                    };
                    var ball = ballPool.GetBall(type);
                    ball.SetSize(randSize);
                    ball.SetRotate(randRotate);
                    var randSpeed = Random.Range(2f, 6f);
                    BallMove(ball, randSpeed, token).Forget();
                    await MyHelper.WaitSeconds(0.1f, token);
                }
            }
        }

        async UniTask BallMove(Ball ball, float speed, CancellationToken token)
        {
            var randX = Random.Range(-7.5f, 7.5f);
            float t = 0f;
            while(t < 5f && ball.gameObject.activeInHierarchy)
            {
                ball.transform.localPosition = new Vector3(randX, 8 - t * t * speed);
                t += Time.deltaTime;
                await UniTask.Yield(token);
            }
            ball.gameObject.SetActive(false);
        }

        async UniTask BallMoveInFever(Ball ball, float speed, CancellationToken token)
        {
            var randX = Random.Range(-7.5f, 7.5f);
            float t = 0f;
            while (t < 5f && ball.gameObject.activeInHierarchy)
            {
                ball.transform.localPosition = new Vector3(randX, 8 - t * t * speed * 2f);
                t += Time.deltaTime;
                await UniTask.Yield(token);
            }
            ball.gameObject.SetActive(false);
        }
    }
}