using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cysharp.Threading.Tasks;

namespace Mirin
{
    public class TutorialCanvas : MonoBehaviour
    {
        [SerializeField] Image frameImage;

        public async UniTask ShowTutorial()
        {
            gameObject.SetActive(true);
            await frameImage.transform.DOLocalMoveX(0f, 0.5f)
                .From(2160f).SetEase(Ease.OutQuad);
            await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
            await frameImage.transform.DOLocalMoveX(-2160f, 0.5f)
                .SetEase(Ease.InQuad);
            gameObject.SetActive(false);
        }
    }
}