using UnityEngine;

/// <summary>
/// ��{�I�ɑS�ẴV�[���ɂ��̃v���n�u��u��
/// �V�[���J�n���Ƀ��X�g�ɓ����Ă���v���n�u��ݒu����
/// �����l�ŊJ�����Ă��R���t���N�g���N�����ɃV���O���g�����C�N�ȃI�u�W�F�N�g��������
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