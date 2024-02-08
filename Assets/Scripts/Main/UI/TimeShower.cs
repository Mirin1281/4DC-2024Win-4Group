using UnityEngine;
using TMPro;

namespace Mirin
{
    public class TimeShower : MonoBehaviour
    {
        [SerializeField] TMP_Text tmpro;
        [SerializeField] Timer timer;

        void Start()
        {
            timer.OnTimeUpdated += UpdateText;
        }

        void UpdateText(float time)
        {
            tmpro.SetText($"�̂���: {MyHelper.GameTime - time:F1}s");
        }
    }
}