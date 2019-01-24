﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour {

    public static ChoiceManager instance;

    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion Singleton

    private AudioManager theAudio;//사운드 재생

    private string question;
    private List<string> answerList;

    public GameObject go; //평소에 비활성화 시킬 목적으로 서언 초이스매니저 자체를
    //set actiove로


    public Text question_Text;
    public Text[] answer_Text;
    public GameObject[] answer_Panel; //텍스트 밑에 선택된것만 짙게 표현위해서.

    public Animator anim;

    public string keySound;
    public string enterSound;

    public bool choiceIng; //선택지 이루어지기 전에 다른 대화 대기
    //()=> !choiceing;
    public bool keyInput; //키처리 활성화.

    private int count; //배열의 크기

    private int result; //선택한 선택창 1,2,3,4

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    // Use this for initialization
    void Start () {
        theAudio = FindObjectOfType<AudioManager>();
        answerList = new List<string>();
        for (int i=0; i<=3; i++)
        {
            answer_Text[i].text = "";
            answer_Panel[i].SetActive(false);
        }
        question_Text.text = "";
	}
	
    public void ShowChocie(Choice _choice)
    {
        choiceIng = true;
        go.SetActive(true);
        result = 0;
        question = _choice.question;
        for (int i=0; i<_choice.answers.Length; i++)
        {
            answerList.Add(_choice.answers[i]);
            answer_Panel[i].SetActive(true);
            count = i;
        }

        anim.SetBool("Appear", true);
        Selection();
        StartCoroutine(ChoiceCoroutine());
    }

    public int GetResult()
    {
        return result;
    }

    public void ExitChoice()
    {
        question_Text.text = "";
        for (int i=0; i<count; i++)
        {
            answer_Text[i].text = "";
            answer_Panel[i].SetActive(false);
        }

        
        anim.SetBool("Appear", false);
        choiceIng = false;
        go.SetActive(false);
        answerList.Clear();
    }

    IEnumerator ChoiceCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(TypingQuestion());
        StartCoroutine(TypingAnswer_0());
        if (count>=1)
            StartCoroutine(TypingAnswer_1());
        if (count >= 2)
            StartCoroutine(TypingAnswer_2());
        if (count >= 3)
            StartCoroutine(TypingAnswer_3());

        yield return new WaitForSeconds(0.2f);

        keyInput = true;
    }

    IEnumerator TypingQuestion()
    {
        for (int i=0; i<question.Length; i++)
        {
            question_Text.text += question[i];
            yield return waitTime;
        }
    }

    IEnumerator TypingAnswer_0()
    {
        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < answerList[0].Length; i++)
        {
            answer_Text[0].text += answerList[0][i];
            yield return waitTime;
        }
    }

    IEnumerator TypingAnswer_1()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < answerList[1].Length; i++)
        {
            answer_Text[1].text += answerList[1][i];
            yield return waitTime;
        }
    }

    IEnumerator TypingAnswer_2()
    {
        yield return new WaitForSeconds(0.6f);
        for (int i = 0; i < answerList[2].Length; i++)
        {
            answer_Text[2].text += answerList[2][i];
            yield return waitTime;
        }
    }


    IEnumerator TypingAnswer_3()
    {
        yield return new WaitForSeconds(0.7f);
        for (int i = 0; i < answerList[3].Length; i++)
        {
            answer_Text[3].text += answerList[3][i];
            yield return waitTime;
        }
    }

    // Update is called once per frame
    void Update () {
		if (keyInput)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                theAudio.Play(keySound);
                if (result > 0)
                    result--;
                else
                    result = count;
                Selection();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Selection();
                theAudio.Play(keySound);
                if (result < count)
                    result++;
                else
                    result = 0;
                Selection();
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                theAudio.Play(enterSound);
                keyInput = false;
                ExitChoice();
            }
        }
	}

    public void Selection()
    {
        Color color = answer_Panel[0].GetComponent<Image>().color;
        color.a = 0.75f;
        for (int i=0; i<=count; i++)
        {
            answer_Panel[i].GetComponent<Image>().color = color;
        }
        color.a = 1f;
        answer_Panel[result].GetComponent<Image>().color = color;
    }
}
