using UnityEngine;
using TMPro;

namespace Mirin
{
    public class ClickShower : MonoBehaviour
    {
        [SerializeField] TMP_Text tmpro;
        [SerializeField] MouseInput mouseInput;

        void Start()
        {
            mouseInput.OnClicked += UpdateText;
        }

        void UpdateText(int clickCount)
        {
            tmpro.SetText($"クリック回数: {clickCount:000}回");
        }
    }
}