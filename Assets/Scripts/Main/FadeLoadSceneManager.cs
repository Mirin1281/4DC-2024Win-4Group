using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

namespace Mirin
{
    public class FadeLoadSceneManager : SingletonMonoBehaviour<FadeLoadSceneManager>
    {
        [SerializeField] Image fadeImage;

        // �t�F�[�h���{�^���̑��d�N���b�N���֎~���邽�߂Ɏg��
        public bool IsSceneChanging { get; private set; }

        /// <summary>
        /// �V�[���J�ڂ�����֐�
        /// </summary>
        /// <param name="fadeInInterval">�Ó]�̕b��</param>
        /// <param name="sceneName">�J�ڐ�̃V�[����</param>
        /// <param name="fadeOutInterval">���]�̕b��(�ȗ�����ƃt�F�[�h�C���Ɠ���)</param>
        /// <param name="fadeColor">�t�F�[�h�̐F(�f�t�H���g�͍�)</param>
        public void LoadScene(float fadeInInterval, string sceneName, float fadeOutInterval = -1f, Color? fadeColor = null)
        {
            LoadSceneAsync(fadeInInterval, sceneName, fadeOutInterval, fadeColor).Forget();
        }

        // ��������await�ł���
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