using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Story : MonoBehaviour
{
    //変数生成
    public Fade fade1;
    public Text Maintext;
    public GameObject panel;
    public GameObject girlpanel;
    public GameObject InPanel;
    public int count = 0;

    void Start()
    {
        StartCoroutine("Color_FadeIn");
    }

    void Update()
    {
        //左クリックを受け付ける
        if (Input.GetMouseButtonDown(0)){
            Debug.Log("クリックされたよ");
            Invoke("Text", 1.0f);
        }
    }

    public void Text()
    {
        count++;
        switch(count)
        {
            case 1:
                StartCoroutine("Color_FadeOut");
            break;
            case 2:
                Maintext.text = ("-◯月✕日AM6：00-");
            break; 
            case 3:
                Maintext.text = ("ん～眠いなぁ");
            break;
            case 4:
                Maintext.text = ("はぁ。大学だるいなぁ");
            break;
            case 5:
                Maintext.text = ("布団から出たくないなぁ");
            break;
            case 6:
                Maintext.text = ("");
                panel.SetActive(false);
                fade1.fade();
            break;
            case 7:
                SceneManager.LoadSceneAsync("MainScene");
            break;
        }
        CancelInvoke();
    }

    IEnumerator Color_FadeIn()
    {
        // 色を変えるゲームオブジェクトからImageコンポーネントを取得
        Image fade = GetComponent<Image>();

        try {
            // フェード元の色を設定（黒）★変更可
            fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (255.0f / 255.0f));
        }
        catch {

        }

        // フェードインにかかる時間（秒）★変更可
        const float fade_time = 2.0f;

        // ループ回数（0はエラー）★変更可
        const int loop_count = 50;

        // ウェイト時間算出
        float wait_time = fade_time / loop_count;

        // 色の間隔を算出
        float alpha_interval = 255.0f / loop_count;

        // 色を徐々に変えるループ
        for (float alpha = 255.0f; alpha >= 0.0f; alpha -= alpha_interval)
        {
            // 待ち時間
            yield return new WaitForSeconds(wait_time);

            try {
            // Alpha値を少しずつ下げる
            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;
            }
            catch {

            }
        }
        CancelInvoke();
    }

    IEnumerator Color_FadeOut()
    {
        // 画面をフェードインさせるコールチン
        // 前提：画面を覆うPanelにアタッチしている

        // 色を変えるゲームオブジェクトからImageコンポーネントを取得
        Image fade = GetComponent<Image>();

        try {
            // フェード元の色を設定（黒）★変更可
            fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (255.0f / 255.0f));
        }
        catch {

        }

        // フェードインにかかる時間（秒）★変更可
        const float fade_time = 2.0f;

        // ループ回数（0はエラー）★変更可
        const int loop_count = 50;

        // ウェイト時間算出
        float wait_time = fade_time / loop_count;

        // 色の間隔を算出
        float alpha_interval = 255.0f / loop_count;

        // 色を徐々に変えるループ
        for (float alpha = 0.0f; alpha <= 255.0f; alpha += alpha_interval)
        {
            // 待ち時間
            yield return new WaitForSeconds(wait_time);

            try {
            // Alpha値を少しずつ下げる
            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;
            }
            catch {

            }
        }
        panel.SetActive(true);
        girlpanel.SetActive(false);
        InPanel.SetActive(false);
    }
}