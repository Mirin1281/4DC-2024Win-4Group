using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

namespace Mirin
{
    public class MouseInput : MonoBehaviour
    {
        [SerializeField] ScoreManager scoreManager;
        [SerializeField] Camera mainCamera;
        [SerializeField] FeverManager feverManager;
        bool isLoop;
        public bool IsLoop
        {
            get => isLoop;
            set
            {
                isLoop = value;
                if (value == true)
                {
                    LoopCheckAsync().Forget();
                }
            }
        }

        int combo;

        public void ResetComboCount()
        {
            combo = 0;
            OnClicked?.Invoke(combo);
        }

        /// <summary>
        /// à¯êîÇÕÉRÉìÉ{êî
        /// </summary>
        public event Action<int> OnClicked;

        async UniTask LoopCheckAsync()
        {
            var token = this.GetCancellationTokenOnDestroy();
            while(IsLoop)
            {
                CheckMouseDown();
                await UniTask.Yield(token);
            }
        }

        void CheckMouseDown()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D[] hit2ds = Physics2D.RaycastAll(ray.origin, ray.direction);

                bool isHit = false;
                foreach(var hit2d in hit2ds)
                {
                    if (hit2d && hit2d.transform.CompareTag("Ball"))
                    {
                        isHit = true;
                        var ball = hit2d.transform.GetComponent<Ball>();
                        ball.OnClicked();
                    }
                }

                if (feverManager.IsFeverMode) return;
                if(isHit)
                {
                    combo++;
                }
                else
                {
                    combo = 0;
                }
                OnClicked?.Invoke(combo);
            }
        }
    }
}