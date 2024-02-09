using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mirin;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private int score;                 // �X�R�A�i�[�p
    [SerializeField] private Text scoreText;

    [SerializeField] private int[] border = { 500000, 800000 };     // �{�[�_�[
    [SerializeField] private GameObject[] girlPanel;                // ���̎q�̉摜�I�u�W�F�N�g
    [SerializeField] private GameObject[] fukidashi;                // �ӂ������I�u�W�F�N�g

    [SerializeField] private int countUpFrame = 90;     // ���Z�A�j���[�V�����ɂ����鎞��[f]

    [SerializeField] private Color[] scoreTextColor = { 
        new Color32(74, 176, 233, 255), 
        new Color32(238, 215, 87, 255), 
        new Color32(255, 136, 211, 255) 
    };

    private string[] commentLv0 = {
        "�N�����Ă��Č����������I", 
        "�N�����Ă��Č����������I",
        "�N�����Ă��Č����������I",
        "��A���I�`�Ȃ́I�H" 
    };

    private string[] commentLv1 = {
        "�x����...", 
        "����A��������Ȏ���...", 
        "����������ƐQ������...",
        "��...�܂�������...",
    };

    private string[] commentLv2 = {
        "�������������Ȃ�", 
        "�悭�Q�ꂽ�`�I",
        "���̖��܂��������Ȃ�",
        "�Ȃ񂩐g�̂��y���悤��...�H",
        "������w�x�񂶂Ⴈ�I"
    };

    // Start is called before the first frame update
    void Start()
    {
        ScoreShow();
    }

    // Update is called once per frame
    void Update()
    {
        // �f�o�b�O�p�����[�h (4���ڂɃR�����g�A�E�g)
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }

    void ScoreShow()
    {
        // score�Ɏ��ۂ̃X�R�A�����鏈��
        if (GameManager.Instance != null)
        {
            score = GameManager.Instance.Score;
        }
        else
        {
            Debug.LogWarning("�X�R�A���擾�ł��܂���ł����B���X�R�A���g�p���܂�");
        }

        // �Z���t���f
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

        // �X�R�A���Z���o
        StartCoroutine(CountUpScore());
    }

    string SelectFukidashiComment(int level)
    {
        string selectedComment = "�R�����g";

        // ����������
        Random.InitState(System.DateTime.Now.Millisecond);

        // ���x�����ƂɃ����_���\��
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

        // �\����1�b�҂�
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
