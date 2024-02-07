using UnityEngine;
using TMPro;

namespace Mirin
{
    public class ScoreShower : MonoBehaviour
    {
        [SerializeField] TMP_Text tmpro;
        [SerializeField] ScoreManager scoreManager;

        void Start()
        {
            scoreManager.OnScoreChanged += UpdateText;
        }

        void UpdateText(int score)
        {
            tmpro.SetText($"Score: {score:0000000}");
        }
    }
}