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
        int Combo
        {
            get => combo;
            set
            {
                combo = Mathf.Clamp(value, 0, MyHelper.FeverCount);
                OnComboCountChanged?.Invoke(Combo);
            }
        }

        int clickCount;
        public event Action<int> OnClicked;

        public void ResetComboCount()
        {
            Combo = 0;
        }

        /// <summary>
        /// à¯êîÇÕÉRÉìÉ{êî
        /// </summary>
        public event Action<int> OnComboCountChanged;

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
            if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && IsEnabled)
            {
                clickCount++;
                OnClicked?.Invoke(clickCount);

                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D[] hit2ds = Physics2D.RaycastAll(ray.origin, ray.direction);

                bool isHit = false;
                foreach (var hit2d in hit2ds)
                {
                    if (hit2d && hit2d.transform.CompareTag("Ball"))
                    {
                        isHit = true;
                        var ball = hit2d.transform.GetComponent<Ball>();
                        ball.OnClicked();
                    }
                }

                int rand = UnityEngine.Random.Range(1, 101);
                if (isHit)
                {
                    if (feverManager.IsFeverMode)
                    {
                        var seType = rand switch
                        {
                            <= 2 => SEType.Piiiii,
                            <= 20 => SEType.FeverBallClick5,
                            <= 40 => SEType.FeverBallClick4,
                            <= 60 => SEType.FeverBallClick3,
                            <= 80 => SEType.FeverBallClick2,
                            _ => SEType.FeverBallClick,
                        };
                        SEManager.Instance.PlaySE(seType);
                        return;
                    }
                    else
                    {
                        var seType = rand switch
                        {
                            <= 33 => SEType.BallClick3,
                            <= 66 => SEType.BallClick2,
                            _ => SEType.BallClick,
                        };
                        SEManager.Instance.PlaySE(seType);
                    }
                    Combo++;
                }
                else
                {
                    var seType = rand switch
                    {
                        <= 50 => SEType.EmptyClick,
                        _ => SEType.EmptyClick2,
                    };
                    SEManager.Instance.PlaySE(seType);
                    if (feverManager.IsFeverMode) return;
                    Combo -= 5;
                }
            }
        }
    }
}