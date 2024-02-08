using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using KanKikuchi.AudioManager;

namespace Mirin
{
    public class FeverManager : MonoBehaviour
    {
        [SerializeField] MouseInput mouseInput;
        [SerializeField] Timer timer;
        [SerializeField] float feverTime = 8f;
        [SerializeField] BallCreator ballCreator;
        [SerializeField] FeverCanvas feverCanvas;
        [SerializeField] ComboSlider comboSlider;
        [SerializeField] BlackPlatePool blackPlatePool;
        [SerializeField] BackScroll backScroll;

        public bool IsFeverMode { get; private set; }

        bool firstFever = true;

        void Start()
        {
            mouseInput.OnComboCountChanged += CheckComboCount;
        }

        void CheckComboCount(int comboCount)
        {
            if(comboCount == MyHelper.FeverCount)
            {
                Fever().Forget();
            }
        }

        async UniTask Fever()
        {
            BGMManager.Instance.Pause(BGMPath.FOURDC302);
            IsFeverMode = true;
            mouseInput.IsEnabled = false;
            timer.AddTime = false;

            if(firstFever)
            {
                BGMManager.Instance.Play(
                    BGMPath.CLUB_F, volumeRate: 0.6f, allowsDuplicate: true);
                firstFever = false;
            }
            else
            {
                BGMManager.Instance.UnPause(BGMPath.CLUB_F);
            }
            var audioSources = BGMManager.Instance.GetComponents<AudioSource>();
            audioSources[1].time = 8f;

            Time.timeScale = 0f;
            feverCanvas.ShowCanvas().Forget();
            CreateBlackPlateAsync(feverTime).Forget();
            await UniTask.Delay(1500, true);

            backScroll.SetSpeedAsync(feverTime).Forget();
            mouseInput.IsEnabled = true;
            feverCanvas.CloseCanvas();
            Time.timeScale = 1f;

            comboSlider.RainbowAsync(feverTime).Forget();
            ballCreator.IsLoop = false;
            ballCreator.IsFever = true;
            await MyHelper.WaitSeconds(feverTime, default);

            mouseInput.IsEnabled = false;
            ballCreator.IsFever = false;
            ballCreator.IsLoop = true;
            timer.AddTime = true;
            mouseInput.IsEnabled = true;
            mouseInput.ResetComboCount();
            IsFeverMode = false;
            BGMManager.Instance.Pause(BGMPath.CLUB_F);
            BGMManager.Instance.UnPause(BGMPath.FOURDC302);
        }

        async UniTask CreateBlackPlateAsync(float time)
        {
            for(int i = 0; i < (time + 1) * 10; i++)
            {
                var plate = blackPlatePool.GetPlate();
                Move(plate).Forget();
                var randWait = Random.Range(10, 90);
                await UniTask.Delay(randWait, true);
            }

            for (int i = 0; i < (time + 1) * 20; i++)
            {
                var plate = blackPlatePool.GetPlate();
                Move(plate).Forget();
                var randWait = Random.Range(5, 45);
                await UniTask.Delay(randWait, true);
            }

            async UniTask Move(Component plt)
            {
                var randDir = Random.Range(0f, 360f);
                var randSize = Random.Range(200f, 1200f);
                plt.transform.localPosition =
                    randSize * new Vector3(MyHelper.Cos(randDir), MyHelper.Sin(randDir));
                await UniTask.Delay(800, true);
                plt.gameObject.SetActive(false);
            }
        }
    }
}