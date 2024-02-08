using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UiAnimation : MonoBehaviour
{
    /// <summary>
    /// 出現時にアニメーションさせたいUIにアタッチさせる
    /// animationNumで実行するアニメーションの種類を指定
    /// </summary>

    public int animationNum = 0;    // アクティブになった時，実行するアニメーションの番号 0は何もしない
    public float timeIn = 0.5f;     // アニメーションにかける時間
    public int moveDistance = 2000; // UI移動距離
    RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        // アクティブになった時に実行するアニメーション群
        // 右からスクロール
        if (animationNum == 1)
        {
            EnableScrollStraight(moveDistance, 0);
        }

        // 下からスクロール
        else if (animationNum == 2)
        {
            EnableScrollStraight(0, -moveDistance);
        }

        // 左からスクロール
        else if (animationNum == 3)
        {
            EnableScrollStraight(-moveDistance, 0);
        }

        // 上からスクロール
        else if (animationNum == 4)
        {
            EnableScrollStraight(0, moveDistance);
        }
    }

    // 4方向の出現時スクロール
    private void EnableScrollStraight(float xStart, float yStart)
    {
        // エディタで設定した位置を取得
        Vector2 myPos = GetMyPos();

        // 開始位置にUIをセット
        this.transform.localPosition = SetStartPos(myPos, xStart, yStart);
        // DebugLogScroll(myPos);

        // UIがアクティブになったらアニメーションで移動させる
        rectTransform.DOLocalMove(myPos, timeIn).SetLink(this.gameObject, LinkBehaviour.RestartOnEnable);
    }

    // 出したい座標位置(エディタで設定した位置)を取得
    private Vector2 GetMyPos()
    {
        Transform myTransform = this.transform;
        return myTransform.localPosition;
    }

    // 第二，三引数でスクロールの開始位置をmyPosから相対座標的に指定
    private Vector2 SetStartPos(Vector2 myPos, float xStart, float yStart)
    {
        myPos.x += xStart;
        myPos.y += yStart;
        return myPos;
    }

    // 移動先座標とスタート座標をログに出力 (デバッグ)
    private void DebugLogScroll(Vector2 myPos)
    {
        Debug.Log("myPos = " + myPos);
        Debug.Log("startPos = " + this.transform.localPosition);
    }
}