using UnityEngine;

namespace Mirin
{
    [CreateAssetMenu(
        fileName = "BallSpriteData",
        menuName = "ScriptableObject/BallSpriteData")
    ]
    public class BallSpriteTypeDat : Enum2ObjectDataBase<BallSpriteType, Sprite>
    {
        /// <summary>
        /// 列挙子を設定します
        /// </summary>
        [ContextMenu("◆SetEnum")]
        protected override void OnContextMenu()
        {
            SetEnum();
        }
    }

    public enum BallSpriteType
    {
        [InspectorName("なし")] None,
        [InspectorName("青1")] Blue1,
        [InspectorName("紫1")] Purple1,
        [InspectorName("赤1")] Red1,
        [InspectorName("黄色1")] Yellow1,
        [InspectorName("黄緑1")] YelGre1,
        [InspectorName("予備2")] _Space2,
        [InspectorName("アウト1")] Out1,
        [InspectorName("アウト2")] Out2,
        [InspectorName("アウト3")] Out3,
        [InspectorName("エンペンメン")] Anpan,
        [InspectorName("R18")] R18,
        [InspectorName("旧ツイッター")] X,
    }
}
