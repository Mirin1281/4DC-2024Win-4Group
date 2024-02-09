using UnityEngine;

namespace Mirin
{
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        public bool IsFirstWatchTutorial { get; set; } = true;
        public int Score { get; set; }
    }
}