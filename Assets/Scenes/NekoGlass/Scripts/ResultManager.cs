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

    [SerializeField] private int[] border = { 500000, 800000 };
    [SerializeField] private GameObject[] girlPanel;
    [SerializeField] private GameObject[] fukidashi;

    [SerializeField] private int countUpFrame = 90;     // ���Z�A�j���[�V�����ɂ����鎞��[f]

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
        if (GameManager.Instance.score != null)
        {
            score = GameManager.Instance.Score;
        }
        else
        {
            Debug.LogError("�X�R�A���擾�ł��܂���ł���");
        }

        // �Z���t���f
        

        // �X�R�A���Z���o
        StartCoroutine(CountUpScore());
    }

    IEnumerator CountUpScore()
    {
        scoreText.text = "0";

        // �\����1�b�҂�
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
