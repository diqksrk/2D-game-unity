using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orderManager : MonoBehaviour {

    

    private PlayerManager thePlayer; //이벤트 중간에 움직임 방지
    private List<MovingObject> characters; //배열은 안된다 a는 10마리 b마을에는 20마리 있을수 있으니.리스트로 한다 그래서 가동성이 좋은

	// Use this for initialization
	void Start () {
        thePlayer = FindObjectOfType<PlayerManager>();
	}

    public void PreLoadCharacter()
    {
        characters = ToList();
    }

    public List<MovingObject> ToList()
    {
        List<MovingObject> tempList = new List<MovingObject>();
        MovingObject[] temp = FindObjectsOfType<MovingObject>();

        for (int i=0; i<temp.Length; i++)
        {
            tempList.Add(temp[i]);
        }
        return tempList;
    }

    public void NotMove()
    {
        thePlayer.notMove = true;
    }

    public void Move()
    {
        thePlayer.notMove = false;
    }

    public void Move(string _name, string _dir)
    {
        for (int i=0; i<characters.Count; i++)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].Move(_dir);
            }
        }
    }

    public void Turn(string _name, string _dir)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].animator.SetFloat("Dirx", 0f);
                characters[i].animator.SetFloat("Diry", 0f);
                switch (_dir)
                {

                    case "UP":
                        characters[i].animator.SetFloat("Diry", 1f);
                        break;
                    case "DOWN":
                        characters[i].animator.SetFloat("Diry", -1f);
                        break;
                    case "LEFT":
                        characters[i].animator.SetFloat("Dirx", -1f);
                        break;
                    case "RIGHT":
                        characters[i].animator.SetFloat("Dirx", 1f);
                        break;
                }
                
            }
        }
    }

    public void SetTransparent(string _name)
    {
        for (int i=0; i<characters.Count; i++)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].boxCollider.enabled = false;
            }
        }
    }

    public void SetUnTransparent(string _name)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].boxCollider.enabled = true;
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
