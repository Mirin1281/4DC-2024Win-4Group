using UnityEngine;

namespace Mirin
{
    public class GamaManager : SingletonMonoBehaviour<GamaManager>
    {
        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public bool IsFirstWatchTutorial { get; set; } = true;
        public int Score { get; set; }
    }
}