using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

namespace Mirin
{
    public class FadeLoadSceneManager : SingletonMonoBehaviour<FadeLoadSceneManager>
    {
        [SerializeField] Image fadeImage;

        // フェード中ボタンの多重クリックを禁止するために使う
        public bool IsSceneChanging { get; private set; }

        /// <summary>
        /// シーン遷移させる関数
        /// </summary>
        /// <param name="fadeInInterval">暗転の秒数</param>
        /// <param name="sceneName">遷移先のシーン名</param>
        /// <param name="fadeOutInterval">明転の秒数(省略するとフェードインと同じ)</param>
        /// <param name="fadeColor">フェードの色(デフォルトは黒)</param>
        public void LoadScene(float fadeInInterval, string sceneName, float fadeOutInterval = -1f, Color? fadeColor = null)
        {
            LoadSceneAsync(fadeInInterval, sceneName, fadeOutInterval, fadeColor).Forget();
        }

        // こっちはawaitできる
        public async UniTask LoadSceneAsync(float fadeInInterval, string sceneName, float fadeOutInterval = -1f, Color? fadeColor = null)
        {
            IsSceneChanging = true;
            if (fadeOutInterval == -1f)
            {
                fadeOutInterval = fadeInInterval;
            }

            if (fadeInInterval != 0f)
            {
                await FadeIn(fadeInInterval, fadeColor);
            }

            await SceneManager.LoadSceneAsync(sceneName);

            if (fadeOutInterval != 0f)
            {
                await FadeOut(fadeOutInterval, fadeColor);
            }
            IsSceneChanging = false;
        }

        public async UniTask FadeIn(float interval, Color? fadeColor = null)
        {
            fadeImage.gameObject.SetActive(true);
            var time = 0f;
            while (time <= interval)
            {
                fadeImage.color = Color.Lerp(Color.clear, fadeColor ?? Color.black, time / interval);
                time += Time.deltaTime;
                await UniTask.Yield();
            }
            fadeImage.color = fadeColor ?? Color.black;
        }

        public async UniTask FadeOut(float interval, Color? fadeColor = null)
        {
            var time = 0f;
            while (time <= interval)
            {
                fadeImage.color = Color.Lerp(fadeColor ?? Color.black, Color.clear, time / interval);
                time += Time.deltaTime;
                await UniTask.Yield();
            }
            fadeImage.gameObject.SetActive(false);
        }
    }
}