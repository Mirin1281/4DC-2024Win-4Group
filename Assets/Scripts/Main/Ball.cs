using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Mirin
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] Collider2D col;
        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] ScoreManager scoreManager;
        [SerializeField] int score = 100000;

        void OnMouseDown()
        {
            /*scoreManager.GetScore(score);
            gameObject.SetActive(false);
            Debug.Log(0);*/
        }

        public void SetScoreManager(ScoreManager sManager)
        {
            scoreManager = sManager;
        }

        public void SetRotate(float deg)
        {
            transform.localRotation = Quaternion.AngleAxis(deg, Vector3.forward);
        }

        public void SetSize(float size)
        {
            transform.localScale = Vector3.one * size;
        }

        public void SetSprite(Sprite sprite)
        {
            spriteRenderer.sprite = sprite;
        }

        public void SetAlpha(float alpha)
        {
            spriteRenderer.color = new Color(1, 1, 1, alpha);
        }

        public void SetCollider(bool enable)
        {
            col.enabled = enable;
        }

        public void SetOrder(int order)
        {
            spriteRenderer.sortingOrder = order + 4;
        }

        public async UniTask LinearMoveAsync(
            Vector2 startPos,
            float deg,
            float speed,
            CancellationToken token,
            float moveTime = 20f
        )
        {
            transform.localPosition = startPos;
            SetRotate(deg);
            var time = 0f;
            var vec = speed * new Vector2(MyStatic.Cos(deg), MyStatic.Sin(deg));
            while (gameObject.activeInHierarchy && time < moveTime)
            {
                transform.localPosition = startPos + time * vec;
                time += Time.deltaTime;
                await UniTask.Yield(token);
            }
        }

        public async UniTask LinearMoveAsync(
            Vector2 startPos,
            float deg,
            IEasable ease,
            CancellationToken token,
            float moveTime = 20f
        )
        {
            transform.localPosition = startPos;
            SetRotate(deg);
            var time = 0f;
            var vec = new Vector2(MyStatic.Cos(deg), MyStatic.Sin(deg));
            while (gameObject.activeInHierarchy && time < moveTime)
            {
                transform.localPosition = startPos + vec * ease.Ease(time);
                time += Time.deltaTime;
                await UniTask.Yield(token);
            }
        }
    }
}