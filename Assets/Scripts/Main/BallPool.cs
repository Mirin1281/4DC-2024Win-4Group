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
                BallSpriteType.Blue1 => 20,
                BallSpriteType.Purple1 => 200,
                BallSpriteType.Red1 => 1000,
                BallSpriteType.Yellow1 => 3000,
                BallSpriteType.YelGre1 => 10,
                BallSpriteType.Out1 => 50,
                BallSpriteType.Out2 => 500,
                BallSpriteType.Out3 => 1000,
                BallSpriteType.Anpan => 5000,
                BallSpriteType.R18 => 1800,
                BallSpriteType.X => 3000,
                BallSpriteType.Money => 4000,
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
