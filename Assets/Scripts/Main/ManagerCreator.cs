using UnityEngine;

/// <summary>
/// 基本的に全てのシーンにこのプレハブを置く
/// シーン開始時にリストに入っているプレハブを設置する
/// 複数人で開発してもコンフリクトが起きずにシングルトンライクなオブジェクトを扱える
/// </summary>
public class ManagerCreator : MonoBehaviour
{
    static bool Loaded { get; set; }
    [SerializeField] CreateManagerData[] managerDatas;

    void Awake()
    {
        foreach (var data in managerDatas)
        {
            if (data.IsDontDestroy && Loaded) continue;

            var obj = Instantiate(data.ManagerPrefab);
            if(data.IsDontDestroy)
            {
                DontDestroyOnLoad(obj);
            }
        }
        Loaded = true;
    }

    [System.Serializable]
    class CreateManagerData
    {
        [field: SerializeField] public GameObject ManagerPrefab { get; private set; }
        [field: SerializeField] public bool IsDontDestroy { get; private set; }
    }
}