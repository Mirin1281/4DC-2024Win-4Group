using UnityEngine;
using System.Collections.Generic;

namespace Mirin
{
    public abstract class PoolBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] T prefab;

        [SerializeField, Tooltip("���O�ɐ������Ă�����")]
        int prepare;

        [SerializeField, Tooltip("���O�ɐ������Ă��������𒴉߂����ꍇ�Ƀ��O���o��")]
        bool showLog = true;

        protected List<T> PooledList;
        int searchIndex;

        protected T GetInstance()
        {
            var listCount = PooledList.Count;

            // ��{�͎g���܂킷
            // Prepare�𒴉߂���Ȃ�for�𔲂���
            for (int i = 0; i < listCount; i++)
            {
                if (searchIndex >= listCount)
                {
                    searchIndex = 0;
                }
                var t = PooledList[searchIndex];
                searchIndex++;
                if (t.gameObject.activeInHierarchy) continue; //�g�p���Ȃ玟

                t.gameObject.SetActive(true);
                return t;
            }

#if UNITY_EDITOR
            if(showLog)
                Debug.Log("Prepare�𒴂���" + typeof(T).Name + "�𐶐����܂�");
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
        /// �V�����C���X�^���X�𐶐����܂�
        /// </summary>
        /// <param name="isActive">�������ɃA�N�e�B�u�ɂ��邩</param>
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