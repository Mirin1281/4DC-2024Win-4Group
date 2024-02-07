using UnityEngine;

public class FeverCanvas : MonoBehaviour
{
    public void ShowCanvas()
    {
        gameObject.SetActive(true);
    }

    public void CloseCanvas()
    {
        gameObject.SetActive(false);
    }
}
