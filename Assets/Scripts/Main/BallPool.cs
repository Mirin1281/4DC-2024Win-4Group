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
                BallSpriteType.Blue1 => 100,
                BallSpriteType.Purple1 => 500,
                BallSpriteType.Red1 => 3000,
                BallSpriteType.Yellow1 => 5000,
                BallSpriteType.YelGre1 => 20,
                BallSpriteType.Out1 => 20,
                BallSpriteType.Out2 => 200,
                BallSpriteType.Out3 => 1000,
                BallSpriteType.Anpan => 3000,
                BallSpriteType.R18 => 1800,
                BallSpriteType.X => 1000,
                BallSpriteType.Money => 3000,
                _ => throw new System.Exception()
            };
            ball.SetScore(score);
            ball.SetType(type);
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
