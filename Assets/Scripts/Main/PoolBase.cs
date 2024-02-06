using UnityEngine;
using System.Collections.Generic;

namespace Mirin
{
    public abstract class PoolBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] T prefab;

        [SerializeField, Tooltip("事前に生成しておく数")]
        int prepare;

        [SerializeField, Tooltip("事前に生成しておいた数を超過した場合にログを出す")]
        bool showLog = true;

        protected List<T> PooledList;
        int searchIndex;

        protected T GetInstance()
        {
            var listCount = PooledList.Count;

            // 基本は使いまわす
            // Prepareを超過するならforを抜ける
            for (int i = 0; i < listCount; i++)
            {
                if (searchIndex >= listCount)
                {
                    searchIndex = 0;
                }
                var t = PooledList[searchIndex];
                searchIndex++;
                if (t.gameObject.activeInHierarchy) continue; //使用中なら次

                t.gameObject.SetActive(true);
                return t;
            }

#if UNITY_EDITOR
            if(showLog)
                Debug.Log("Prepareを超えて" + typeof(T).Name + "を生成します");
#endif
            return NewInstantiate(true);
        }

        void Awake()
        {
            PooledList = new List<T>(prepare);
            StartInstance();
        }

        void StartInstance()
        {
            for (int i = 0; i < prepare; i++)
            {
                NewInstantiate(false);
            }
        }

        /// <summary>
        /// 新しくインスタンスを生成します
        /// </summary>
        /// <param name="isActive">生成時にアクティブにするか</param>
        /// <returns></returns>
        T NewInstantiate(bool isActive)
        {
            var t = Instantiate(prefab, transform);
            t.gameObject.SetActive(isActive);
            PooledList.Add(t);
            return t;
        }
    }
}