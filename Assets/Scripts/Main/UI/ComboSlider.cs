using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

namespace Mirin
{
    public class ComboSlider : MonoBehaviour
    {
        [SerializeField] Slider slider;
        [SerializeField] MouseInput mouseInput;
        [SerializeField] Image fillImage;

        private void Start()
        {
            slider.maxValue = MyHelper.FeverCount;
            fillImage.color = Color.green;
            mouseInput.OnComboCountChanged += UpdateSliderValue;
        }

        void UpdateSliderValue(int comboCount)
        {
            slider.value = comboCount;
        }

        public async UniTask RainbowAsync(float feverTime)
        {
            slider.wholeNumbers = false;
            float t = 0f;
            float s = 0f;
            while (t < feverTime)
            {
                slider.value = MyHelper.FeverCount - t / feverTime * MyHelper.FeverCount;
                if (s > 1)
                {
                    s = 0f;
                }
                fillImage.color = Color.HSVToRGB(s, 1, 1);

                t += Time.deltaTime;
                s += Time.deltaTime;
                await UniTask.Yield();
            }
            slider.wholeNumbers = true;
            fillImage.color = Color.green;
        }
    }
}