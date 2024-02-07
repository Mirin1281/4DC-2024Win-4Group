using UnityEngine;

namespace Mirin
{
    public class BallPool : PoolBase<Ball>
    {
        [SerializeField] BallSpriteTypeDat typeData;
        [SerializeField] ScoreManager scoreManager;

        public Ball GetBall(BallSpriteType type)
        {
            Ball ball = GetInstance();
            ball.SetSprite(typeData.GetObject(type));
            var score = type switch
            {
                BallSpriteType.None => 0,
                BallSpriteType.Blue1 => 1000,
                BallSpriteType.Purple1 => 2000,
                BallSpriteType.Red1 => 3000,
                BallSpriteType.Yellow1 => 5000,
                BallSpriteType.Out1 => 3000,
                BallSpriteType.Out2 => 7000,
                BallSpriteType.Out3 => 10000,
                _ => throw new System.Exception()
            };
            ball.SetScore(score);
            ball.SetOrder(Random.Range(0, 10));
            ball.SetScoreManager(scoreManager);
            return ball;
        }

        public void InActiveAllBalls()
        {
            foreach (var ball in PooledList)
            {
                if (ball.gameObject.activeInHierarchy)
                {
                    ball.gameObject.SetActive(false);
                }
            }
        }
    }
}
