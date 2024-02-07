using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Mirin
{
    public class Particle : MonoBehaviour
    {
        [SerializeField] ParticleSystem particle;

        public void PlayPaticle(float time = 0.3f)
        {
            PlayPaticleAsync(time).Forget();
        }

        async UniTask PlayPaticleAsync(float time)
        {
            particle.Play();
            await MyHelper.WaitSeconds(time, default);
            Destroy(gameObject);
        }
    }
}