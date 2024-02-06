using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Mirin
{
    public class Test : MonoBehaviour
    {
        async UniTask Start()
        {
            var ballPool = MyStatic.FindComponent<BallPool>();
            var token = this.GetCancellationTokenOnDestroy();
            while (true)
            {
                var ball = ballPool.GetBall(BallSpriteType.Blue1);
                BallMove(ball, token).Forget();
                await MyStatic.WaitSeconds(0.1f, token);
            }
        }

        async UniTask BallMove(Ball ball, CancellationToken token)
        {
            var randX = Random.Range(-7.5f, 7.5f);
            await ball.LinearMoveAsync(new Vector2(randX, 7f), 270, 12f, token, 5f);
            ball.gameObject.SetActive(false);

        }
    }
}