using UnityEngine;

namespace Mirin
{
    public class BallPool : PoolBase<Ball>
    {
        [SerializeField] BallSpriteTypeData typeData;

        public Ball GetBall(BallSpriteType type)
        {
            Ball ball = GetInstance();
            ball.SetSize(1);
            ball.SetSprite(typeData.GetObject(type));
            ball.SetCollider(true);
            ball.SetAlpha(1f);
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
