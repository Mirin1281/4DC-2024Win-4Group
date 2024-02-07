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
        /// ρqπέθ΅ά·
        /// </summary>
        [ContextMenu("SetEnum")]
        protected override void OnContextMenu()
        {
            SetEnum();
        }
    }

    public enum BallSpriteType
    {
        [InspectorName("Θ΅")] None,
        [InspectorName("Β1")] Blue1,
        [InspectorName("1")] Purple1,
        [InspectorName("Τ1")] Red1,
        [InspectorName("©F1")] Yellow1,
        [InspectorName("\υ1")] _Space1,
        [InspectorName("\υ2")] _Space2,
        [InspectorName("AEg1")] Out1,
        [InspectorName("AEg2")] Out2,
        [InspectorName("AEg3")] Out3,
        [InspectorName("Gy")] Anpan,
        [InspectorName("R18")] R18,
    }
}
