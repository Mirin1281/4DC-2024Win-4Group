using UnityEngine;

public class SEManager : SingletonMonoBehaviour<SEManager>
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] SETypeData seData;

    /// <summary>
    /// SE‚ð–Â‚ç‚µ‚Ü‚·
    /// </summary>
    /// <param name="type">SETypeŒ^‚ÅŽw’è</param>
    /// <param name="volumeRate"></param>
    public void PlaySE(SEType type, float volumeRate = 1f)
    {
        var (se, vol) = seData.GetSE(type);
        audioSource.PlayOneShot(se, volumeRate * vol);
    }

    protected override void Awake()
    {
        base.Awake();
        if (audioSource is null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }
}
