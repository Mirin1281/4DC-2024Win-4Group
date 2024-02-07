using UnityEngine;
#if UNITY_EDITOR
using UnityEditor; // AssetDatabaseを使うために必要
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
        /// シーン内のコンポーネントを検索します
        /// </summary>
        /// <typeparam name="T">コンポーネント</typeparam>
        /// <param name="objName">コンポーネントのついたオブジェクトの名前(コンポーネント名と同じなら省略可)</param>
        /// <param name="findInactive">非アクティブも検索する</param>
        /// <param name="callLog">呼ばれた際にログを出す</param>
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
                    ColorLogWarning(findName + "でFindしましたが、オブジェクトが見つかりませんでした");
                }
                return default;
            }

            var t = obj.GetComponent<T>();
            if (t is null)
            {
                if (callLog)
                {
                    ColorLogWarning(findName + "でFindしましたが、コンポーネントが見つかりませんでした");
                }
                return default;
            }
            if (callLog)
            {
                ColorLog(typeof(T).Name + "をFindしました");
            }

            return t;
        }

        /// <summary>
        /// 非アクティブのも含めてFindします
        /// </summary>
        /// <param name="targetName"></param>
        /// <returns></returns>
        static GameObject FindIncludInactive(string targetName)
        {
            var gameObjects = Resources.FindObjectsOfTypeAll<GameObject>();

            foreach (var gameObjectInHierarchy in gameObjects)
            {

#if UNITY_EDITOR
                //Hierarchy上のものでなければスルー
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