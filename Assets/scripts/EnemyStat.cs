using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStat : MonoBehaviour {

    public int hp;
    public int currentHP;
    public int atk;
    public int def;
    public int exp;

    public GameObject healthBarBackground;
    public Image healthBarFilled;


	// Use this for initialization
	void Start () {
        currentHP = hp;
        healthBarFilled.fillAmount = 1f;
	}

    public int Hit(int _playerAtk)
    {
        int playerAtk = _playerAtk;
        int dmg;
        if (def >= playerAtk)
            dmg = 1;
        else
            dmg = playerAtk - def;

        currentHP -= dmg;

        if (currentHP <= 0)
        {
            Destroy(this.gameObject);
            PlayerStat.instance.currentEXP += exp;
        }

        healthBarFilled.fillAmount = (float)currentHP/hp;
        healthBarBackground.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(WaitCoroutine());
        return dmg;
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(3f);
        healthBarBackground.SetActive(false);
    }
}
