using UnityEngine;

namespace Mirin
{
    public enum BallSpriteType
    {
        [InspectorName("‚È‚µ")] None,
        [InspectorName("Â1")] Blue1,
        [InspectorName("Ô1")] Red1,
    }

    [CreateAssetMenu(
        fileName = "BallSpriteData",
        menuName = "ScriptableObject/BallSpriteData")
    ]
    public class BallSpriteTypeData : Enum2ObjectDataBase<BallSpriteType, Sprite>
    {
        /// <summary>
        /// —ñ‹“q‚ğİ’è‚µ‚Ü‚·
        /// </summary>
        [ContextMenu("ŸSetEnum")]
        protected override void OnContextMenu()
        {
            SetEnum();
        }
    }
}
