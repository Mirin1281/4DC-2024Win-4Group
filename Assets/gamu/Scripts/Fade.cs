using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public GameObject panel;
    public void fade()
    {
        StartCoroutine("Color_FadeIn");
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
}
