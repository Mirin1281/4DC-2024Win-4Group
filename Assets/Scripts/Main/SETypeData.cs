using UnityEngine;
using System;
using System.Collections.Generic;

namespace Mirin
{
    public enum SEType
    {
        [InspectorName("なし")] None,
        [InspectorName("玉破壊音")] BallClick,
        [InspectorName("空振り")] EmptyClick,
        [InspectorName("確定")] Pati,
    }

    [CreateAssetMenu(
        fileName = "SEData",
        menuName = "ScriptableObject/SEData")
    ]

    public class SETypeData : ScriptableObject
    {
        [SerializeField] float masterVol = 1f;

        [Header("右上の「︙」 > 「◆SetEnum」から列挙子を更新できます")]
        [SerializeField] List<LinkedSE> linkedSeList;

        [Serializable]
        class LinkedSE
        {
            [SerializeField] SEType type;

            [SerializeField] AudioClip se;

            [SerializeField] float volumeRate = 1f;

            public void SetType(SEType type)
            {
                this.type = type;
            }

            public AudioClip GetSE() => se;

            public float GetVolume() => volumeRate;
        }

        /// <summary>
        /// 列挙子を設定します
        /// </summary>
        [ContextMenu("◆SetEnum")]
        void SetEnum()
        {
            int enumCount = Enum.GetValues(typeof(SEType)).Length;
            if (linkedSeList == null) linkedSeList = new();
            int deltaCount = 1; // 仮置き
            while (deltaCount != 0)
            {
                deltaCount = linkedSeList.Count - enumCount;
                if (deltaCount > 0)
                {
                    linkedSeList.RemoveAt(enumCount);
                }
                else if (deltaCount < 0)
                {
                    linkedSeList.Add(new LinkedSE());
                }
            }

            for (int i = 0; i < enumCount; i++)
            {
                linkedSeList[i].SetType((SEType)i);
            }
        }

        public (AudioClip, float) GetSE(SEType type)
        {
            var linkedSe = linkedSeList[(int)type];
            return (linkedSe.GetSE(), linkedSe.GetVolume() * masterVol);
        }
    }
}