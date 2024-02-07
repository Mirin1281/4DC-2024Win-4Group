using UnityEngine;

namespace Mirin
{
    public enum BallSpriteType
    {
        [InspectorName("�Ȃ�")] None,
        [InspectorName("��1")] Blue1,
        [InspectorName("�A�E�g1")] Out1,
    }

    [CreateAssetMenu(
        fileName = "BallSpriteData",
        menuName = "ScriptableObject/BallSpriteData")
    ]
    public class BallSpriteTypeData : Enum2ObjectDataBase<BallSpriteType, Sprite>
    {
        /// <summary>
        /// �񋓎q��ݒ肵�܂�
        /// </summary>
        [ContextMenu("��SetEnum")]
        protected override void OnContextMenu()
        {
            SetEnum();
        }
    }
}
