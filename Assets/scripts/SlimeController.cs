using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MovingObject {

    public float attackDelay; //공격 유예.

    public float inter_MoveWaitTime; //대기 시간
    private float current_inteMWT;

    public string atkSound;

    private Vector2 PlayerPos; //플레이어 좌표값

    private int random_int;
    private string direction;

    public GameObject healthBar;


	// Use this for initialization
	void Start () {
        queue = new Queue<string>();
        current_inteMWT = inter_MoveWaitTime;
	}
	
	// Update is called once per frame
	void Update () {

        current_inteMWT -= Time.deltaTime;

        if (current_inteMWT <= 0)
        {
            current_inteMWT = inter_MoveWaitTime;

            if (NearPlayer())
            {
                Flip();
                return;
            }

            RandmoDirection();

            if (base.CheckColision())
                return;
            base.Move(direction);
        }
	}

    private void Flip()
    {
        Vector3 flip = transform.localScale;
        if (PlayerPos.x > this.transform.position.x)
        {
            flip.x = -1f;
        }else
        {
            flip.x = 1f;
        }
        this.transform.localScale = flip;
        healthBar.transform.localScale = flip;
        animator.SetTrigger("Attack");
        StartCoroutine(WaitCoroutine());
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(attackDelay);
        AudioManager.instance.Play(atkSound);
        if (NearPlayer())
        {
            PlayerStat.instance.Hit(GetComponent<EnemyStat>().atk);
        }
    }

    private bool NearPlayer()
    {
        PlayerPos = PlayerManager.instance.transform.position;

        if (Mathf.Abs(Mathf.Abs(PlayerPos.x) - Mathf.Abs(this.transform.position.x))<= speed * walkCount*1.01f)
        {
            if (Mathf.Abs(Mathf.Abs(PlayerPos.y) - Mathf.Abs(this.transform.position.y)) <= speed * walkCount*0.5f)
            {
                return true;
            }
        }
        if (Mathf.Abs(Mathf.Abs(PlayerPos.y) - Mathf.Abs(this.transform.position.y)) <= speed * walkCount * 1.01f)
        {
            if (Mathf.Abs(Mathf.Abs(PlayerPos.x) - Mathf.Abs(this.transform.position.x)) <= speed * walkCount * 0.5f)
            {
                return true;
            }
        }
        return false;
    }

    private void RandmoDirection()
    {
        vector.Set(0, 0, vector.z);
        random_int = Random.Range(0, 4);
        switch (random_int)
        {
            case 0:
                vector.y = 1f;
                direction = "UP";
                break;
            case 1:
                vector.y = -1f;
                direction = "DOWN";
                break;
            case 2:
                vector.x = 1f;
                direction = "RIGHT";
                break;
            case 3:
                vector.x = -1f;
                direction = "LEFT";
                break;
        }
    }
}
