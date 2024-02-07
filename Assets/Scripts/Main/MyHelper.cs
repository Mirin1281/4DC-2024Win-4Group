using UnityEngine;
#if UNITY_EDITOR
using UnityEditor; // AssetDatabase���g�����߂ɕK�v
#endif
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Mirin
{
    public static class MyHelper
    {
        public const float GameTime = 10f;
        public const int FeverCount = 10;

        public static float Sin(float deg) => Mathf.Sin(deg * Mathf.Deg2Rad);
        public static float Cos(float deg) => Mathf.Cos(deg * Mathf.Deg2Rad);

        public static UniTask WaitSeconds(float wait, CancellationToken token)
            => UniTask.Delay(System.TimeSpan.FromSeconds(wait), cancellationToken: token);

        /// <summary>
        /// �V�[�����̃R���|�[�l���g���������܂�
        /// </summary>
        /// <typeparam name="T">�R���|�[�l���g</typeparam>
        /// <param name="objName">�R���|�[�l���g�̂����I�u�W�F�N�g�̖��O(�R���|�[�l���g���Ɠ����Ȃ�ȗ���)</param>
        /// <param name="findInactive">��A�N�e�B�u����������</param>
        /// <param name="callLog">�Ă΂ꂽ�ۂɃ��O���o��</param>
        /// <returns></returns>
        public static T FindComponent<T>(
            string objName = null, bool findInactive = true, bool callLog = true)
        {
#if UNITY_EDITOR
#else
        callLog = false;
#endif
            var findName = objName ?? typeof(T).Name;
            var obj = GameObject.Find(findName);
            if (obj is null && findInactive)
            {
                obj = FindIncludInactive(findName);
            }
            if (obj is null)
            {
                if (callLog)
                {
                    ColorLogWarning(findName + "��Find���܂������A�I�u�W�F�N�g��������܂���ł���");
                }
                return default;
            }

            var t = obj.GetComponent<T>();
            if (t is null)
            {
                if (callLog)
                {
                    ColorLogWarning(findName + "��Find���܂������A�R���|�[�l���g��������܂���ł���");
                }
                return default;
            }
            if (callLog)
            {
                ColorLog(typeof(T).Name + "��Find���܂���");
            }

            return t;
        }

        /// <summary>
        /// ��A�N�e�B�u�̂��܂߂�Find���܂�
        /// </summary>
        /// <param name="targetName"></param>
        /// <returns></returns>
        static GameObject FindIncludInactive(string targetName)
        {
            var gameObjects = Resources.FindObjectsOfTypeAll<GameObject>();

            foreach (var gameObjectInHierarchy in gameObjects)
            {

#if UNITY_EDITOR
                //Hierarchy��̂��̂łȂ���΃X���[
                if (!AssetDatabase.GetAssetOrScenePath(gameObjectInHierarchy).Contains(".unity"))
                {
                    continue;
                }
#endif
                if (gameObjectInHierarchy.name == targetName)
                {
                    return gameObjectInHierarchy;
                }
            }
            return null;
        }

        static void ColorLog(string str)
        {
            Debug.Log("<color=lightblue>" + str + "</color>");
        }
        static void ColorLogWarning(string str)
        {
            Debug.LogWarning("<color=red>" + str + "</color>");
        }
    }
}