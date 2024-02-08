using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace Mirin
{
    public class ReadyCanvas : MonoBehaviour
    {
        [SerializeField] Image readyImage;
        [SerializeField] Image goImage;

        public async UniTask ShowReady()
        {
            gameObject.SetActive(true);
            readyImage.gameObject.SetActive(true);
            goImage.gameObject.SetActive(false);

            await readyImage.transform.DOScale(0.6f, 0.8f).SetEase(Ease.InQuad);

            readyImage.gameObject.SetActive(false);
            goImage.gameObject.SetActive(true);
            SEManager.Instance.PlaySE(SEType.Start);

            _ = goImage.transform.DOScale(2f, 0.7f).From(0.6f).SetEase(Ease.OutQuad);
            await MyHelper.WaitSeconds(0.4f, default);
            await goImage.DOFade(0f, 0.3f);
            gameObject.SetActive(false);


        }
    }
}