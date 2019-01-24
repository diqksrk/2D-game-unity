using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCMove
{
    [Tooltip("NPCMove를 클릭하면 npc가 움직임")]
    public bool NPCmove;

    public string[] direction; // npc가 움직일 방향

    [Range(1,5)] [Tooltip("1=천천히, 2=조금천천히, 3=보통, 4=빠르게, 5=연속적으로")]
    public int frequency; //npc가 얼마나 빠른속도로 움직일 것인가 1칸움직였다가 기다렸다 1칸움직이고 이런것
}

public class NPCManager : MovingObject {

    [SerializeField]
    public NPCMove npc;

	// Use this for initialization
	void Start () {
        queue = new Queue<string>();
        StartCoroutine(MoveCoroutine());
    }

    public void SetMove()
    {

    }

    public void SetNotMove()
    {

    }

    

    IEnumerator MoveCoroutine()
    {
        if (npc.direction.Length != 0)
        {
            for (int i=0; i<=npc.direction.Length; i++)
            {
                

                yield return new WaitUntil(() => queue.Count<2);
                base.Move(npc.direction[i], npc.frequency);

                if (i == npc.direction.Length-1)
                {
                    i = -1;
                }

            }
        }
    }
}
