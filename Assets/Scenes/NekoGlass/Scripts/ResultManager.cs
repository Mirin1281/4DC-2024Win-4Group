using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private int score;                 // �X�R�A�i�[�p
    [SerializeField] private Text scoreText;

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
    }


}
