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

        void UpdateText(int getScore)
        {
            tmpro.SetText($"‚·‚±‚  : {scoreManager.Score:000000}");
        }
    }
}