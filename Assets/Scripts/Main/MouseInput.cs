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
        /// 引数はコンボ数
        /// </summary>
        public event Action<int> OnClicked;

        public bool IsEnabled { get; set; } = true;

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
            if (Input.GetMouseButtonDown(0) && IsEnabled)
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

                
                if(isHit)
                {
                    SEManager.Instance.PlaySE(SEType.BallClick);
                    if (feverManager.IsFeverMode) return;
                    combo++;
                }
                else
                {
                    if (feverManager.IsFeverMode) return;
                    combo = 0;
                }
                OnClicked?.Invoke(combo);
            }
        }
    }
}