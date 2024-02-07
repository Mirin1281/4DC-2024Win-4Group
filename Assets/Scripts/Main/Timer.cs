using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

namespace Mirin
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] float currentTime;
        public float CurrentTime => currentTime;

        public event Action<float> OnTimeUpdated;

        bool addTime;
        public bool AddTime
        {
            set
            {
                addTime = value;
                if (value == false) return;
                UpdateTimerAsync().Forget();
            }
        }

        async UniTask UpdateTimerAsync()
        {
            var token = this.GetCancellationTokenOnDestroy();
            while (addTime)
            {
                currentTime += Time.deltaTime;
                OnTimeUpdated?.Invoke(CurrentTime);
                await UniTask.Yield(token);
            }
        }

        public void ResetTimer()
        {
            currentTime = 0;
        }
    }
}
