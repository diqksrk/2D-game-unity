using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour {

    public static PlayerStat instance;

    public int character_Lv;
    public int[] needExp;
    public int currentEXP;

    public int hp;
    public int currentHP;
    public int mp;
    public int currentMP;

    public int atk;
    public int def;

    public int recover_hp; //1초
    public int recover_mp;

    public string dmgSound;

    public float time;
    private float current_time;

    public GameObject prefabs_Floating_text;
    public GameObject parent;

    public Slider hpSlider;
    public Slider mpSlder;

	// Use this for initialization
	void Start () {
        instance = this;
        currentHP = hp;
        currentMP = mp;
        current_time = time;
	}
	
    public void Hit(int _enemyAtk)
    {
        int dmg;

        if (def >= _enemyAtk)
            dmg = 1;
        else
            dmg = _enemyAtk - def;

        currentHP -= dmg;

        if (currentHP <= 0)
        {
            Debug.Log("체력 0미만, 게임오버");
        }
        AudioManager.instance.Play(dmgSound);

        Vector3 vector = this.transform.position;
        vector.y += 60;

        GameObject clone = Instantiate(prefabs_Floating_text, vector, Quaternion.Euler(Vector3.zero));
        clone.transform.position = vector;
        clone.GetComponent<FloatingText>().text.text = dmg.ToString();
        clone.GetComponent<FloatingText>().text.color = Color.red;
        clone.GetComponent<FloatingText>().text.fontSize = 25;
        clone.transform.SetParent(parent.transform);
        StopAllCoroutines();
        StartCoroutine(HitCoroutine());


    }

    IEnumerator HitCoroutine()
    {
        Color color = GetComponent<SpriteRenderer>().color;
        for (int i=0; i<3; i++)
        {
            if (i == 2)
            {
                color.a = 0;
                GetComponent<SpriteRenderer>().color = color;
                yield return new WaitForSeconds(0.1f);
                color.a = 1f;
                GetComponent<SpriteRenderer>().color = color;
            }
            color.a = 0;
            GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(0.1f);
            color.a = 1f;
            GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Update is called once per frame
    void Update () {

        hpSlider.maxValue = hp;
        mpSlder.maxValue = mp;

        hpSlider.value = currentHP;
        mpSlder.value = currentMP;

        if (currentEXP >= needExp[character_Lv])
        {
            character_Lv++;
            hp += character_Lv * 2;
            mp += character_Lv + 2;

            currentHP = hp;
            currentMP = mp;
        }
        current_time -= Time.deltaTime;

        if (current_time<=0)
        {
            if (recover_hp > 0)
            {
                if (currentHP + recover_hp <= hp)
                    currentHP += recover_hp;
                else
                    currentHP = hp;

            }
            current_time = time;
        }
	}
}
