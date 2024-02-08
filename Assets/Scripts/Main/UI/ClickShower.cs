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
            tmpro.SetText($"�N���b�N��: {clickCount:000}��");
        }
    }
}