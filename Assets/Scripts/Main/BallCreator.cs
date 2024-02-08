using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Mirin
{
    public class BallCreator : MonoBehaviour
    {
        [SerializeField] BallPool ballPool;
        [SerializeField] BallSpriteTypeDat typeData;

        bool isFever;
        public bool IsFever
        {
            private get => isFever;
            set
            {
                isFever = value;
                if (value == true)
                {
                    LoopFeverCreateAsync().Forget();
                }
            }
        }

        bool isLoop;
        public bool IsLoop
        {
            private get => isLoop;
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
                var randSize = Random.Range(0.3f, 0.6f);
                var randTypeNum = Random.Range(1, 101);
                var randRotate = Random.Range(0f, 360f);
                var type = randTypeNum switch
                {
                    <= 2 => BallSpriteType.Yellow1,
                    <= 8 => BallSpriteType.Red1,
                    <= 20 => BallSpriteType.Purple1,
                    <= 50 => BallSpriteType.Blue1,
                    _ => BallSpriteType.YelGre1,
                };
                var ball = ballPool.GetBall(type);
                ball.SetSize(randSize);
                ball.SetRotate(randRotate);
                BallMove(ball, token).Forget();
                await MyHelper.WaitSeconds(0.08f, token);
            }
        }

        async UniTask BallMove(Ball ball, CancellationToken token)
        {
            var randX = Random.Range(-7.5f, 7.5f);
            var randSpeed = Random.Range(3f, 8f);
            var randRotate = Random.Range(-500f, 500f);
            var randMoveX = Random.Range(-0.5f, 0.5f);
            float t = 0f;
            while(t < 5f && ball.gameObject.activeInHierarchy)
            {
                ball.transform.localPosition = new Vector3(randX + randMoveX * t, 8 - t * t * randSpeed);
                ball.SetRotate(t * randRotate);
                t += Time.deltaTime;
                await UniTask.Yield(token);
            }
            ball.gameObject.SetActive(false);
        }

        async UniTask LoopFeverCreateAsync()
        {
            var token = this.GetCancellationTokenOnDestroy();
            float t = 0f;
            while (IsFever)
            {
                var s = t / 8f + 1f;
                var type = Random.Range(1, 1001) switch
                {
                    <= 2 => BallSpriteType.Anpan,
                    <= 6 => BallSpriteType.Money,
                    <= 15 => BallSpriteType.X,
                    <= 30 => BallSpriteType.R18,
                    <= 60 => BallSpriteType.Out1,
                    <= 300 => BallSpriteType.Out2,
                    _ => BallSpriteType.Out3,
                };
                bool isRare = type == BallSpriteType.Anpan || type == BallSpriteType.Money;
                var ball = ballPool.GetBall(type);
                ball.SetRotate(Random.Range(0f, 360f));
                BallMoveInFever(ball, s, isRare, token).Forget();
                t += 0.03f;
                await MyHelper.WaitSeconds(0.03f, token);
            }
        }

        async UniTask BallMoveInFever(Ball ball, float time, bool isRare, CancellationToken token)
        {
            var randX = Random.Range(-7.5f, 7.5f);
            var randSpeed = isRare ?
                Random.Range(2f, 4f) :
                Random.Range(4f, 10f) * time * time;
            if(isRare)
            {
                ball.SetSize(Random.Range(0.2f, 0.6f));
            }
            else
            {
                ball.SetSize(Random.Range(0.1f, 0.5f) * time);
            }
            var randRotate = Random.Range(-500f, 500f);
            var randMoveX = Random.Range(-0.5f, 0.5f) * time;
            float t = 0f;
            while (t < 5f && ball.gameObject.activeInHierarchy)
            {
                ball.transform.localPosition = new Vector3(randX + randMoveX * t, 8 - t * t * randSpeed);
                ball.SetRotate(t * randRotate);
                t += Time.deltaTime;
                await UniTask.Yield(token);
            }
            ball.gameObject.SetActive(false);
        }
    }
}