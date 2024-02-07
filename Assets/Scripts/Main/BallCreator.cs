using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Mirin
{
    public class BallCreator : MonoBehaviour
    {
        [SerializeField] BallPool ballPool;
        [SerializeField] float speed = 1f;

        public async UniTask LoopCreateAsync()
        {
            var token = this.GetCancellationTokenOnDestroy();
            while (true)
            {
                var ball = ballPool.GetBall(BallSpriteType.Blue1);
                BallMove(ball, token).Forget();
                await MyHelper.WaitSeconds(0.1f, token);
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
    }
}