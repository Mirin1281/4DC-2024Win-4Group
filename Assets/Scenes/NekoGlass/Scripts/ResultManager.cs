using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mirin;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private int score;                 // スコア格納用
    [SerializeField] private Text scoreText;

    [SerializeField] private int[] border = { 500000, 800000 };
    [SerializeField] private GameObject[] girlPanel;
    [SerializeField] private GameObject[] fukidashi;

    [SerializeField] private int countUpFrame = 90;     // 加算アニメーションにかける時間[f]

    // Start is called before the first frame update
    void Start()
    {
        ScoreShow();
    }

    // Update is called once per frame
    void Update()
    {
        // デバッグ用リロード (4日目にコメントアウト)
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void ScoreShow()
    {
        // scoreに実際のスコアを入れる処理
        if (GameManager.Instance.score != null)
        {
            score = GameManager.Instance.Score;
        }
        else
        {
            Debug.LogError("スコアを取得できませんでした");
        }

        // セリフ反映
        

        // スコア加算演出
        StartCoroutine(CountUpScore());
    }

    IEnumerator CountUpScore()
    {
        scoreText.text = "0";

        // 表示後1秒待つ
        yield return new WaitForSeconds(1);

        float oneFrameScore = score / countUpFrame;
        float countUpScore = 0;

        for (int i = 0; i < countUpFrame; i++)
        {
            countUpScore += oneFrameScore;
            scoreText.text = Mathf.FloorToInt(countUpScore).ToString();
            yield return null;
        }
        scoreText.text = score.ToString();

        StartCoroutine(ShowGirl());
    }

    IEnumerator ShowGirl()
    {
        yield return new WaitForSeconds(0.5f);

        // LV0
        if (score < border[0])
        {
            fukidashi[0].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            girlPanel[0].gameObject.SetActive(true);

        }
        // LV1
        else if (score < border[1])
        {
            fukidashi[1].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            girlPanel[1].gameObject.SetActive(true);
        }
        // LV2
        else
        {
            fukidashi[2].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            girlPanel[2].gameObject.SetActive(true);
        }
    }

}
