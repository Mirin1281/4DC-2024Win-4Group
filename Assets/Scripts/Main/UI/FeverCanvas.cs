using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class FeverCanvas : MonoBehaviour
{
    [SerializeField] Image image;

    public async UniTask ShowCanvas()
    {
        gameObject.SetActive(true);
        for(int i = 0; i < 6; i++)
        {
            await image.rectTransform.DOLocalMoveY(-1080f, i * 0.05f)
                .From(1080f).SetEase(Ease.InOutQuad).SetUpdate(true);
        }
        _ = image.rectTransform.DOLocalMoveY(0, 9f * 0.05f)
                .From(1080f).SetEase(Ease.InQuad).SetUpdate(true);
    }

    public void CloseCanvas()
    {
        gameObject.SetActive(false);
    }
}
