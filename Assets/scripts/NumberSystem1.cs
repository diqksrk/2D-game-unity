using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberSystem1 : MonoBehaviour {

    private AudioManager theAudio;
    public string key_sound; //방향키 사운드
    public string enter_sound; //결정키 사운드
    public string cancel_sound; //오답 사운드
    public string correct_sound; // 정답사운드

    private int count; //배열의 크기
    private int selectedTextBox; //선택된자리수
    private int result; //프레이어가 도출해낸 값
    private int correctNumber; // 정답

    private string tempNumber;

    public GameObject superObject; //화면 가운데 정렬을 위한 녀석
    public GameObject[] panel;
    public Text[] Number_Text;

    public Animator anim;

    public bool activated;  //return new waituntil을 위한 녀석
    private bool keyInput; //키처리 활성화, 비활성화
    private bool correctFlag; //정답인지 아닌지 여부

    // Use this for initialization
    void Start () {
        theAudio = FindObjectOfType<AudioManager>();		
	}

    public void ShowNumber(int _correctNumber)
    {
        correctNumber = _correctNumber;
        activated = true;
        correctFlag = false;

        string temp = correctNumber.ToString(); //이유 -> length로 쓰려고
        for (int i=0; i<temp.Length; i++)
        {
            count = i;
            panel[i].SetActive(true);
            Number_Text[i].text = "0";
        }
        superObject.transform.position = new Vector3(superObject.transform.position.x + 30 * count,
                                                     superObject.transform.position.y, 
                                                     superObject.transform.position.z);
        selectedTextBox = 0;
        result = 0;
        SetColor();
        anim.SetBool("Appear", true);
        keyInput = true;


    }

    public bool GetResult()
    {
        return correctFlag;
    }

    public void SetNumber(string _arrow)
    {
        int temp = int.Parse(Number_Text[selectedTextBox].text);//선택된 자리의 텍스트를 인트형식으로 강제 형변환

        if (_arrow == "DOWN")
        {
            if (temp == 0)
            {
                temp = 9;
            }
            else
                temp--;
        }else if (_arrow == "UP")
        {
            if (temp == 9)
            {
                temp = 0;
            }
            else
                temp++;
        }
        Number_Text[selectedTextBox].text = temp.ToString();

    }

    public void SetColor()
    {
        Color color = Number_Text[0].color;
        color.a = 0.3f;
        for (int i = 0; i <= count; i++)
        {
            Number_Text[i].color = color;

        }
        color.a = 1f;
        Number_Text[selectedTextBox].color = color;
    }

    // Update is called once per frame
    void Update () {
		if (keyInput)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                theAudio.Play(key_sound);
                SetNumber("DOWN");
            }else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                theAudio.Play(key_sound);
                SetNumber("UP");
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                theAudio.Play(key_sound);
                if (selectedTextBox < count)
                    selectedTextBox++;
                else
                    selectedTextBox = 0;
                SetColor();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                theAudio.Play(key_sound);
                if (selectedTextBox > 0)
                    selectedTextBox--;
                else
                    selectedTextBox = count;
                SetColor();
            }
            else if (Input.GetKeyDown(KeyCode.Z))//결정키
            {
                theAudio.Play(key_sound);
                keyInput = false;
                StartCoroutine(OXCoroutine());
            }
            else if (Input.GetKeyDown(KeyCode.X))//취소키
            {
                theAudio.Play(key_sound);
                keyInput = false;
                StartCoroutine(ExitCoroutine());
            }
        }
	}

    IEnumerator OXCoroutine()
    {
        Color color = Number_Text[0].color;
        color.a = 1f;
        for (int i=count; i>=0; i--)
        {
            
            Number_Text[i].color = color;
            tempNumber += Number_Text[i].text; //5000...>1356
        }
        yield return new WaitForSeconds(1f);
        result = int.Parse(tempNumber);
        if (result == correctNumber)
        {
            theAudio.Play(correct_sound);
            correctFlag = true;
        }
        else
        {
            theAudio.Play(cancel_sound);
            correctFlag = false;
        }
        StartCoroutine(ExitCoroutine());
    }

    IEnumerator ExitCoroutine()
    {
        result = 0;
        tempNumber = "";
        anim.SetBool("Appear", false);
        yield return new WaitForSeconds(0.1f);
        for (int i=0; i<=count; i++)
        {
            panel[i].SetActive(false);
        }
        superObject.transform.position = new Vector3(superObject.transform.position.x - 30 * count,
                                                     superObject.transform.position.y,
                                                     superObject.transform.position.z);
        Debug.Log("우리가 낸답 =" + result + "정답 =" + correctNumber);
        activated = false;
    }
}
