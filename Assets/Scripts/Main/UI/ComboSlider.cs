using UnityEngine;
using UnityEngine.UI;

namespace Mirin
{
    public class ComboSlider : MonoBehaviour
    {
        [SerializeField] Slider slider;
        [SerializeField] MouseInput mouseInput;

        private void Start()
        {
            slider.maxValue = MyHelper.FeverCount;
            mouseInput.OnComboCountChanged += UpdateSliderValue;
        }

        void UpdateSliderValue(int comboCount)
        {
            slider.value = comboCount;
        }
    }
}