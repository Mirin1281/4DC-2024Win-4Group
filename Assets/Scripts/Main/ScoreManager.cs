using UnityEngine;
using TMPro;
using System;
using Cysharp.Threading.Tasks;

namespace Mirin
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] int score;
        //public int Score => score;


        public int Score
        {
            get => score;
            private set
            {
                score = value;
                OnScoreChanged?.Invoke(value);
            }
        }

        public event Action<int> OnScoreChanged;

        public void GetScore(int s)
        {
            GetScoreAsync(s).Forget();
        }

        async UniTask GetScoreAsync(int getScore)
        {
            if (getScore < 1000)
            {
                var pieceScore = (int)(getScore / 10f);
                for (int i = 0; i < 10; i++)
                {
                    Score += pieceScore;
                    await UniTask.Yield();
                }
            }
            else
            {
                var pieceScore = (int)(getScore / 100f);
                for (int i = 0; i < 100; i++)
                {
                    Score += pieceScore;
                    await UniTask.Yield();
                }
            }
        }
    }
}