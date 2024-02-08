using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UiAnimation : MonoBehaviour
{
    /// <summary>
    /// �o�����ɃA�j���[�V������������UI�ɃA�^�b�`������
    /// animationNum�Ŏ��s����A�j���[�V�����̎�ނ��w��
    /// </summary>

    public int animationNum = 0;    // �A�N�e�B�u�ɂȂ������C���s����A�j���[�V�����̔ԍ� 0�͉������Ȃ�
    public float timeIn = 0.5f;     // �A�j���[�V�����ɂ����鎞��
    public int moveDistance = 2000; // UI�ړ�����
    RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        // �A�N�e�B�u�ɂȂ������Ɏ��s����A�j���[�V�����Q
        // �E����X�N���[��
        if (animationNum == 1)
        {
            EnableScrollStraight(moveDistance, 0);
        }

        // ������X�N���[��
        else if (animationNum == 2)
        {
            EnableScrollStraight(0, -moveDistance);
        }

        // ������X�N���[��
        else if (animationNum == 3)
        {
            EnableScrollStraight(-moveDistance, 0);
        }

        // �ォ��X�N���[��
        else if (animationNum == 4)
        {
            EnableScrollStraight(0, moveDistance);
        }
    }

    // 4�����̏o�����X�N���[��
    private void EnableScrollStraight(float xStart, float yStart)
    {
        // �G�f�B�^�Őݒ肵���ʒu���擾
        Vector2 myPos = GetMyPos();

        // �J�n�ʒu��UI���Z�b�g
        this.transform.localPosition = SetStartPos(myPos, xStart, yStart);
        // DebugLogScroll(myPos);

        // UI���A�N�e�B�u�ɂȂ�����A�j���[�V�����ňړ�������
        rectTransform.DOLocalMove(myPos, timeIn).SetLink(this.gameObject, LinkBehaviour.RestartOnEnable);
    }

    // �o���������W�ʒu(�G�f�B�^�Őݒ肵���ʒu)���擾
    private Vector2 GetMyPos()
    {
        Transform myTransform = this.transform;
        return myTransform.localPosition;
    }

    // ���C�O�����ŃX�N���[���̊J�n�ʒu��myPos���瑊�΍��W�I�Ɏw��
    private Vector2 SetStartPos(Vector2 myPos, float xStart, float yStart)
    {
        myPos.x += xStart;
        myPos.y += yStart;
        return myPos;
    }

    // �ړ�����W�ƃX�^�[�g���W�����O�ɏo�� (�f�o�b�O)
    private void DebugLogScroll(Vector2 myPos)
    {
        Debug.Log("myPos = " + myPos);
        Debug.Log("startPos = " + this.transform.localPosition);
    }
}