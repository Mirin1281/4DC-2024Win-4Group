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

    [SerializeField] private int[] border = { 500000, 800000 };     // ボーダー
    [SerializeField] private GameObject[] girlPanel;                // 女の子の画像オブジェクト
    [SerializeField] private GameObject[] fukidashi;                // ふきだしオブジェクト

    [SerializeField] private int countUpFrame = 90;     // 加算アニメーションにかける時間[f]

    [SerializeField] private Color[] scoreTextColor = { 
        new Color32(74, 176, 233, 255), 
        new Color32(238, 215, 87, 255), 
        new Color32(255, 136, 211, 255) 
    };

    private string[] commentLv0 = {
        "起こしてって言ったじゃん！", 
        "起こしてって言ったじゃん！",
        "起こしてって言ったじゃん！",
        "ゆ、夢オチなの！？" 
    };

    private string[] commentLv1 = {
        "遅刻だ...", 
        "あれ、もうこんな時間...", 
        "もうちょっと寝かせて...",
        "ん...まだ眠いよ...",
    };

    private string[] commentLv2 = {
        "いい夢だったなぁ", 
        "よく寝れた～！",
        "この夢また見たいなぁ",
        "なんか身体が軽いような...？",
        "もう大学休んじゃお！"
    };

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
        if (GameManager.Instance != null)
        {
            score = GameManager.Instance.Score;
        }
        else
        {
            Debug.LogWarning("スコアを取得できませんでした。仮スコアを使用します");
        }

        // セリフ反映
        // LV0
        if (score < border[0])
        {
            ApplyFukidashiText(0, SelectFukidashiComment(0));
        }
        // LV1
        else if (score < border[1])
        {
            ApplyFukidashiText(1, SelectFukidashiComment(1));
        }
        // LV2
        else
        {
            ApplyFukidashiText(2, SelectFukidashiComment(2));
        }

        // スコア加算演出
        StartCoroutine(CountUpScore());
    }

    string SelectFukidashiComment(int level)
    {
        string selectedComment = "コメント";

        // 乱数初期化
        Random.InitState(System.DateTime.Now.Millisecond);

        // レベルごとにランダム表示
        if (level == 0)
        {
            selectedComment = commentLv0[Random.Range(0, commentLv0.Length)];
        }
        else if (level == 1)
        {
            selectedComment = commentLv1[Random.Range(0, commentLv1.Length)];
        }
        else
        {
            selectedComment = commentLv2[Random.Range(0, commentLv2.Length)];
        }

        return selectedComment;
    }
    
    void ApplyFukidashiText(int level, string comment)
    {
        Text myFukidashiText = fukidashi[level].GetComponentInChildren<Text>();
        myFukidashiText.text = comment;
    }

    IEnumerator CountUpScore()
    {
        scoreText.text = "0";

        // 表示後1秒待つ
        yield return new WaitForSeconds(1);

        float oneFrameScore = (float)score / countUpFrame;
        float countUpScore = 0;
        int floorScore = 0;

        for (int i = 0; i < countUpFrame; i++)
        {
            countUpScore += oneFrameScore;
            floorScore = Mathf.FloorToInt(countUpScore);

            scoreText.text = floorScore.ToString();
            scoreText.color = JudgeTextColor(floorScore);

            yield return null;
        }

        scoreText.text = score.ToString();
        scoreText.color = JudgeTextColor(score);

        StartCoroutine(ShowGirl());
    }

    Color JudgeTextColor(float countUpScore)
    {
        if (countUpScore < border[0])
        {
            return scoreTextColor[0]; 
        }
        else if (countUpScore < border[1])
        {
            return scoreTextColor[1];
        }
        else
        {
            return scoreTextColor[2];
        }
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
