using UnityEngine;

namespace Mirin
{
    public enum BallSpriteType
    {
        [InspectorName("なし")] None,
        [InspectorName("青1")] Blue1,
        [InspectorName("アウト1")] Out1,
    }

    [CreateAssetMenu(
        fileName = "BallSpriteData",
        menuName = "ScriptableObject/BallSpriteData")
    ]
    public class BallSpriteTypeData : Enum2ObjectDataBase<BallSpriteType, Sprite>
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
}
