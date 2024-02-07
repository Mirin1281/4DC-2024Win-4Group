using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private int score;                 // スコア格納用
    [SerializeField] private Text scoreText;

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
    }


}
