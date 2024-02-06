using UnityEngine;

namespace Mirin
{
    public class BallPool : PoolBase<Ball>
    {
        [SerializeField] BallSpriteTypeData typeData;
        [SerializeField] ScoreManager scoreManager;

        public Ball GetBall(BallSpriteType type)
        {
            Ball ball = GetInstance();
            ball.SetSize(0.3f);
            ball.SetSprite(typeData.GetObject(type));
            /*ball.SetCollider(true);
            ball.SetAlpha(1f);*/
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
