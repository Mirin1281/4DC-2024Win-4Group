using UnityEngine;
using TMPro;
using System;

namespace Mirin
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] int score;
        public int Score => score;
        [SerializeField] TMP_Text tmpro;

        public event Action<int> OnScoreChanged;

        public void GetScore(int s)
        {
            score += s;
        }

        
    }
}