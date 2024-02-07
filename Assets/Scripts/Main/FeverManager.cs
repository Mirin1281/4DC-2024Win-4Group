using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

namespace Mirin
{
    public class FeverManager : MonoBehaviour
    {
        [SerializeField] MouseInput mouseInput;
        [SerializeField] Timer timer;
        [SerializeField] float feverTime = 8f;
        [SerializeField] Image fillImage;
        [SerializeField] BallCreator ballCreator;
        [SerializeField] FeverCanvas feverCanvas;

        public bool IsFeverMode { get; private set; }

        void Start()
        {
            mouseInput.OnComboCountChanged += CheckComboCount;
        }

        void CheckComboCount(int comboCount)
        {
            if(comboCount == MyHelper.FeverCount)
            {
                Fever().Forget();
            }
        }

        async UniTask Fever()
        {
            IsFeverMode = true;
            mouseInput.IsEnabled = false;
            timer.AddTime = false;
            SEManager.Instance.PlaySE(SEType.Pati);
            Time.timeScale = 0f;
            feverCanvas.ShowCanvas().Forget();
            await UniTask.Delay(4000, true);
            mouseInput.IsEnabled = true;
            feverCanvas.CloseCanvas();
            Time.timeScale = 1f;
            
            RainbowAsync().Forget();
            ballCreator.IsFever = true;
            await MyHelper.WaitSeconds(feverTime, default);

            mouseInput.IsEnabled = false;
            ballCreator.IsFever = false;
            timer.AddTime = true;
            mouseInput.IsEnabled = true;
            mouseInput.ResetComboCount();
            IsFeverMode = false;
        }

        async UniTask RainbowAsync()
        {
            float t = 0f;
            float s = 0f;
            while(t < feverTime)
            {
                if(s > 1)
                {
                    s = 0f;
                }
                fillImage.color = Color.HSVToRGB(s, 1, 1);

                t += Time.deltaTime;
                s += Time.deltaTime;
                await UniTask.Yield();
            }
            fillImage.color = Color.green;
        }
    }
}