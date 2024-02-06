using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] int score;
    public int Score => score;

    public void GetScore(int s)
    {
        score += s;
    }
}
