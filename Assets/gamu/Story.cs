using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Story : MonoBehaviour
{
    public Text Maintext;
    public GameObject panel;
    public GameObject Wpanel;
    public GameObject girlpanel;
    public GameObject InPanel;
    public int t = 0;
    public bool flg = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Color_FadeIn");
    }

    // Update is called once per frame
    void Update()
    {
        //左クリックを受け付ける
        if (Input.GetMouseButtonDown(0)){
            Debug.Log("クリックされたよ");
            if(flg)
            {
                StartCoroutine("Color_FadeOut");
                flg = false;
            }
            Invoke("Text", 1.0f);
        }
    }

    public void Text()
    {
        t++;
        switch(t)
        {
            case 2:
                Maintext.text = ("-◯月◯日AM5：00-");
            break; 
            case 3:
                Maintext.text = ("ん～眠いなぁ");
            break;
            case 4:
                Maintext.text = ("はぁ。大学だるいなぁ");
            break;
            case 5:
                Maintext.text = ("");
                Wpanel.SetActive(true);
                panel.SetActive(false);
            break;
            case 6:
                InPanel.SetActive(true);
                StartCoroutine("Color_FadeIn");
            break;
            case 7:
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
        const float fade_time = 1.5f;

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
        const float fade_time = 1.5f;

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
        girlpanel.SetActive(false);
        panel.SetActive(true);
        InPanel.SetActive(false);
    }

    public void Delay()
    {

    }
}