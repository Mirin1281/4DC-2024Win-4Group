using UnityEngine;
using System;
using System.Collections.Generic;

namespace Mirin
{
    /// <summary>
    /// Enum型のT1をキーとしてT2を取り出すことができるクラスです
    /// 継承しているクラスを参照してGetType(T1 t)で取得できます
    /// </summary>
    /// <typeparam name="T1">列挙型</typeparam>
    /// <typeparam name="T2">データ</typeparam>
    public abstract class Enum2ObjectDataBase<T1, T2> : ScriptableObject where T1 : Enum
    {
        [Header("右上の「︙」 > 「◆SetEnum」から列挙子を更新できます")]
        [SerializeField] List<LinkedType> linkedTypes;

        [Serializable]
        public class LinkedType
        {
            [SerializeField]
            T1 enumParam;

            [SerializeField]
            T2 obj;

            public void SetType(T1 p_enumParam)
            {
                enumParam = p_enumParam;
            }

            public T2 GetObject() => obj;
        }

        protected abstract void OnContextMenu();
        protected void SetEnum()
        {
            int enumCount = Enum.GetValues(typeof(T1)).Length;
            if (linkedTypes == null) linkedTypes = new();
            int deltaCount = 1; // 仮置き
            while(deltaCount != 0)
            {
                deltaCount = linkedTypes.Count - enumCount;
                if (deltaCount > 0)
                {
                    linkedTypes.RemoveAt(enumCount);
                }
                else if(deltaCount < 0)
                {
                    linkedTypes.Add(new LinkedType());
                }
            }
            
            for (int i = 0; i < enumCount; i++)
            {
                linkedTypes[i].SetType((T1)Enum.ToObject(typeof(T1), i));
            }
        }

        public T2 GetObject(T1 p_enumParam)
        {
            return linkedTypes[Convert.ToInt32(p_enumParam)].GetObject();
        }
    }
}