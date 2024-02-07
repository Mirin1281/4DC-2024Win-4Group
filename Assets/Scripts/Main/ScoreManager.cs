using UnityEngine;
using TMPro;

namespace Mirin
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] int score;
        public int Score => score;
        [SerializeField] TMP_Text tmpro;

        public void GetScore(int s)
        {
            score += s;
            UpdateText();
        }

        void UpdateText()
        {
            tmpro.SetText(score.ToString());
        }
    }
}