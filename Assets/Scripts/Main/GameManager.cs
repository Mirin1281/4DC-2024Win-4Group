using UnityEngine;

namespace Mirin
{
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public bool IsFirstWatchTutorial { get; set; } = true;
        public int Score { get; set; }
    }
}