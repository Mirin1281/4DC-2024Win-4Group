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
        /// �񋓎q��ݒ肵�܂�
        /// </summary>
        [ContextMenu("��SetEnum")]
        protected override void OnContextMenu()
        {
            SetEnum();
        }
    }

    public enum BallSpriteType
    {
        [InspectorName("�Ȃ�")] None,
        [InspectorName("��1")] Blue1,
        [InspectorName("��1")] Purple1,
        [InspectorName("��1")] Red1,
        [InspectorName("���F1")] Yellow1,
        [InspectorName("����1")] YelGre1,
        [InspectorName("�\��2")] _Space2,
        [InspectorName("�A�E�g1")] Out1,
        [InspectorName("�A�E�g2")] Out2,
        [InspectorName("�A�E�g3")] Out3,
        [InspectorName("�G���y������")] Anpan,
        [InspectorName("R18")] R18,
        [InspectorName("���c�C�b�^�[")] X,
    }
}
